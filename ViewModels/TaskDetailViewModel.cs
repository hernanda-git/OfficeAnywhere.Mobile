using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FFImageLoading.Maui;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class TaskDetailViewModel : ObservableObject
{
    private readonly DelmonTaskCacheService _delmonTaskCacheService;
    public string? DelmonTask { get; private set; }

    [ObservableProperty]
    private bool isBusy = false;

    [ObservableProperty]
    private string? title = "Task Details";

    public event Action? ViewsReady;

    [ObservableProperty]
    private ObservableCollection<View> dynamicContents = new();

    public TaskDetailViewModel(DelmonTaskCacheService delmonTaskCacheService)
    {
        _delmonTaskCacheService = delmonTaskCacheService;

        DelmonTask = _delmonTaskCacheService.GetCachedTask();

        if (DelmonTask is not null)
        {
            _ = InitializeAsync(DelmonTask); // fire-and-forget
        }
        else
        {
            _ = ToListTask();
        }
    }

    private async Task InitializeAsync(string taskJson)
    {
        await Task.Run(() =>
        {
            using var document = JsonDocument.Parse(taskJson);
            JsonElement root = document.RootElement;

            var views = GenerateDetailViews(root);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DynamicContents.Clear();
                foreach (var view in views)
                    DynamicContents.Add(view);

                ViewsReady?.Invoke();
            });
        });
    }

    private async Task ToListTask()
    {
        await Shell.Current.GoToAsync("//TaskPage");
    }

    [RelayCommand]
    public async Task TaskSelectedAsync(TaskCard? selectedTask)
    {
        if (IsBusy || selectedTask == null) return;

        await Shell.Current.GoToAsync("//TaskPage");

        IsBusy = false;
    }

    private IEnumerable<View> GenerateDetailViews(JsonElement json)
    {
        var views = new List<View>();

        string? senderUserImage = json.TryGetProperty("SenderUserImage", out JsonElement senderUserImageElement) ? senderUserImageElement.GetString() : string.Empty;
        string? senderName = json.TryGetProperty("SenderName", out JsonElement senderNameElement) ? senderNameElement.GetString() : string.Empty;
        string? addedDateRaw = json.TryGetProperty("AddedDate", out JsonElement addedDateElement) ? addedDateElement.GetString() : string.Empty;

        string formattedDate = DateTime.TryParse(addedDateRaw, out var parsedDate)
            ? parsedDate.ToString("dd MMM yyyy")
            : string.Empty;

        var profileFrame = CreateProfileImage(senderUserImage);
        var nameDateGrid = CreateNameDateGrid(senderName, formattedDate);
        var taskStateView = CreateTaskDetailCard(json);

        var containerGrid = new Grid
        {
            RowSpacing = 2,
            ColumnSpacing = 5,
            FlowDirection = FlowDirection.RightToLeft,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            }
        };

        Grid.SetRow(profileFrame, 0);
        Grid.SetRowSpan(profileFrame, 2);
        Grid.SetColumn(profileFrame, 1);

        Grid.SetRow(nameDateGrid, 0);
        Grid.SetColumn(nameDateGrid, 0);

        Grid.SetRow(taskStateView, 1);
        Grid.SetColumn(taskStateView, 0);

        containerGrid.Children.Add(profileFrame);
        containerGrid.Children.Add(nameDateGrid);
        containerGrid.Children.Add(taskStateView);

        views.Add(containerGrid);
        return views;
    }
    private Border CreateTaskDetailCard(JsonElement json)
    {
        var border = new Border
        {
            Padding = 10,
            Stroke = new SolidColorBrush(GetPrimaryDark()),
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) }
        };

        string? taskStateName = json.TryGetProperty("TaskStateName", out JsonElement taskStateNameElement) ? taskStateNameElement.GetString() : string.Empty;
        string? color = json.TryGetProperty("Color", out JsonElement ColorElement) ? ColorElement.GetString() : string.Empty;
        string? taskTypeName = json.TryGetProperty("TaskTypeName", out JsonElement taskTypeNameElement) ? taskTypeNameElement.GetString() : string.Empty;
        string? details = json.TryGetProperty("Details", out JsonElement detailsElement) ? detailsElement.GetString() : string.Empty;

        if (!string.IsNullOrEmpty(taskStateName) && !string.IsNullOrEmpty(color))
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                RowSpacing = 10
            };

            var badge = GenerateBadge(taskStateName, color);
            Grid.SetRow(badge, 0);
            Grid.SetColumn(badge, 0);
            grid.Children.Add(badge);

            if (!string.IsNullOrEmpty(taskTypeName))
            {
                var label = GenerateLabel(taskTypeName, FontSizeOption.Description, TextAlignment.End);
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, 1);
                grid.Children.Add(label);
            }

            if (!string.IsNullOrEmpty(details))
            {
                var desc = GenerateLabel(details, FontSizeOption.Medium, TextAlignment.Start);
                Grid.SetRow(desc, 1);
                Grid.SetColumn(desc, 0);
                Grid.SetColumnSpan(desc, 2);
                grid.Children.Add(desc);
            }

            border.Content = grid;
        }

        return border;
    }

    private Frame CreateProfileImage(string? imagePath)
    {
        string imageUrl = string.IsNullOrWhiteSpace(imagePath)
            ? "sample_profile_picture.jpeg"
            : $"https://o-anywhere.com{imagePath}";

        var image = new CachedImage
        {
            Source = imageUrl,
            Aspect = Aspect.AspectFill,
            HeightRequest = 40,
            WidthRequest = 40,
            Transformations = new List<ITransformation> { new CircleTransformation() }
        };

        return new Frame
        {
            Margin = 0,
            Padding = 5,
            CornerRadius = 100,
            HeightRequest = 45,
            WidthRequest = 45,
            HasShadow = false,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            BackgroundColor = GetPrimaryDark(),
            BorderColor = GetPrimaryDark(),
            Content = image
        };
    }

    private Grid CreateNameDateGrid(string? senderName, string formattedDate)
    {
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            RowDefinitions = { new RowDefinition { Height = GridLength.Auto } },
            HorizontalOptions = LayoutOptions.Fill
        };

        var nameLabel = new Label
        {
            Text = senderName,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(5, 0, 0, 0),
            HorizontalTextAlignment = TextAlignment.End,
            HorizontalOptions = LayoutOptions.End
        };

        var dateLabel = new Label
        {
            Text = formattedDate,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(0, 0, 5, 0),
            HorizontalTextAlignment = TextAlignment.Start,
            HorizontalOptions = LayoutOptions.Start
        };

        Grid.SetColumnSpan(nameLabel, 2);
        Grid.SetColumnSpan(dateLabel, 2);

        grid.Children.Add(nameLabel);
        grid.Children.Add(dateLabel);

        return grid;
    }

    private bool IsUrl(string input)
    {
        return Uri.TryCreate(input, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private View GenerateLabel(string text, FontSizeOption? fontSize = null, TextAlignment? alignment = null, Color? textColor = null, string? id = null, Thickness? margin = null)
    {
        var lines = text
            .Replace("<br />", "\n") // Normalize line breaks
            .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var color = textColor ?? Colors.Black;
        var size = (double)(fontSize ?? FontSizeOption.Medium);
        var align = alignment ?? TextAlignment.Start;
        var labelMargin = margin ?? new Thickness(0, 0, 0, 20);

        var layout = new VerticalStackLayout { Spacing = 5 };

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            if (IsUrl(line))
            {
                var linkLabel = new Label
                {
                    Text = line,
                    TextColor = Colors.Blue,
                    FontSize = size,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = align,
                    Margin = margin ?? new Thickness(0, 0, 0, 5),
                    StyleId = !string.IsNullOrEmpty(id) ? $"{id}_link_{i}" : null
                };

                var tap = new TapGestureRecognizer
                {
                    Command = new Command(() => Launcher.Default.OpenAsync(new Uri(line)))
                };
                linkLabel.GestureRecognizers.Add(tap);
                layout.Children.Add(linkLabel);
            }
            else
            {
                layout.Children.Add(new Label
                {
                    Text = line,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = size,
                    TextColor = color,
                    HorizontalTextAlignment = align,
                    Margin = margin ?? new Thickness(0, 0, 0, 5),
                    StyleId = !string.IsNullOrEmpty(id) ? $"{id}_{i}" : null
                });
            }
        }

        return layout;
    }

    private Color GetPrimaryDark()
    {
        if (App.Current?.Resources.TryGetValue("PrimaryDark", out var val) == true && val is Color c)
            return c;

        return Colors.Black;
    }

    private View GenerateBadge(string text, string bootstrapClass, string id = "")
    {
        (Color Background, Color Text) = bootstrapClass switch
        {
            string c when c.Contains("badge-primary") => (Color.FromRgb(0, 123, 255), Colors.White), // Blue
            string c when c.Contains("badge-secondary") => (Color.FromRgb(108, 117, 125), Colors.White), // Gray
            string c when c.Contains("badge-success") => (Color.FromRgb(40, 167, 69), Colors.White), // Green
            string c when c.Contains("badge-danger") => (Color.FromRgb(220, 53, 69), Colors.White), // Red
            string c when c.Contains("badge-warning") => (Colors.Yellow, Colors.Black), // Yellow
            string c when c.Contains("badge-info") => (Color.FromRgb(23, 162, 184), Colors.White), // Cyan
            string c when c.Contains("badge-light") => (Color.FromRgb(248, 249, 250), Colors.Black), // Light gray
            string c when c.Contains("badge-dark") => (Color.FromRgb(52, 58, 64), Colors.White), // Dark gray
            _ => (Colors.Gray, Colors.White)
        };

        var badge = new Label
        {
            Text = text,
            BackgroundColor = Background,
            TextColor = Text,
            FontSize = (double)FontSizeOption.Description,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(8, 4),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.NoWrap
        };

        var border = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) },
            Stroke = Background,
            BackgroundColor = Background,
            Content = badge,
            Padding = new Thickness(2),
            Margin = new Thickness(0, 0, 0, 10)
        };

        if (!string.IsNullOrEmpty(id))
        {
            border.StyleId = id;
        }

        return border;
    }
}
