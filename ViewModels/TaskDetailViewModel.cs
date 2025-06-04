using CommunityToolkit.Mvvm.ComponentModel;
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
    private string? title = "Task Details";


    [ObservableProperty]
    private ObservableCollection<View> dynamicContents = new();

    public TaskDetailViewModel(DelmonTaskCacheService delmonTaskCacheService)
    {
        _delmonTaskCacheService = delmonTaskCacheService;
        DelmonTask = _delmonTaskCacheService.GetCachedTask();

        if (DelmonTask is not null)
        {
            using JsonDocument document = JsonDocument.Parse(DelmonTask);
            JsonElement root = document.RootElement;

            //GenerateDetailElements(root);
        }
        else
        {
            ToListTask();
        }
    }

    private async void ToListTask() => await Shell.Current.GoToAsync("//TaskPage");

    //private void GenerateDetailElements(JsonElement json)
    //{
    //    DynamicContents.Clear();

    //    //string? addedDate = json.TryGetProperty("AddedDate", out JsonElement addedDateElement) ? addedDateElement.GetString() : string.Empty;
    //    //string? endDate = json.TryGetProperty("EndDate", out JsonElement endDateElement) ? endDateElement.GetString() : string.Empty;
    //    //string? lastUpdateDate = json.TryGetProperty("LastUpdatedDate", out JsonElement lastUpdateDateElement) ? lastUpdateDateElement.GetString() : string.Empty;
    //    //string? taskStateId = json.TryGetProperty("TaskStateId", out JsonElement taskStateIdElement) ? taskStateIdElement.GetString() : string.Empty;
    //    //string? taskTypeId = json.TryGetProperty("TaskTypeId", out JsonElement taskTypeIdElement) ? taskTypeIdElement.GetString() : string.Empty;
    //    //string? senderUserImage = json.TryGetProperty("SenderUserImage", out JsonElement senderUserImageElement) ? senderUserImageElement.GetString() : string.Empty;
    //    //string? employUserImage = json.TryGetProperty("EmployUserImage", out JsonElement employUserImageElement) ? employUserImageElement.GetString() : string.Empty;
    //    //string? isRead = json.TryGetProperty("IsRead", out JsonElement isReadElement) ? isReadElement.GetString() : string.Empty;
    //    //string? messageCount = json.TryGetProperty("MessageCount", out JsonElement messageCountElement) ? messageCountElement.GetString() : string.Empty;
    //    //string? formData = json.TryGetProperty("FromData", out JsonElement formDataElement) ? formDataElement.GetString() : string.Empty;
    //    //string? messages = json.TryGetProperty("Messages", out JsonElement messagesElement) ? messagesElement.GetString() : string.Empty;

    //    string? title = json.TryGetProperty("Title", out JsonElement titleElement) ? titleElement.GetString() : string.Empty;
    //    if (!string.IsNullOrEmpty(title))
    //    {
    //        Title = title;
    //    }

    //    var border = new Border();
    //    if (App.Current != null)
    //    {
    //        if (App.Current.Resources.TryGetValue("PrimaryDark", out var primaryDark) && primaryDark is Color primaryDarkColor)
    //        {
    //            border.Background = new SolidColorBrush(primaryDarkColor);
    //        }

    //        border.Stroke = new SolidColorBrush(Colors.Transparent);
    //        border.StrokeThickness = 0;
    //        border.StrokeShape = new RoundRectangle
    //        {
    //            CornerRadius = new CornerRadius(0, 0, 30, 30)
    //        };

    //        border.Padding = new Thickness(20);
    //    }
    //    var stackLayout = new StackLayout();
    //    stackLayout.Padding = new Thickness(0);
    //    stackLayout.Margin = new Thickness(0);
    //    stackLayout.HorizontalOptions = LayoutOptions.Fill;

    //    // badge for task state
    //    string? taskStateName = json.TryGetProperty("TaskStateName", out JsonElement taskStateNameElement) ? taskStateNameElement.GetString() : string.Empty;
    //    string? color = json.TryGetProperty("Color", out JsonElement colorElement) ? colorElement.GetString() : string.Empty;
    //    if (!string.IsNullOrEmpty(taskStateName) && !string.IsNullOrEmpty(color))
    //    {
    //        var grid = new Grid
    //        {
    //            ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Star } },
    //            RowDefinitions = { new RowDefinition { Height = GridLength.Auto } }
    //        };

    //        var badge = GenerateBadge(taskStateName, color);

    //        Grid.SetRow(badge, 0);
    //        Grid.SetColumn(badge, 0);
    //        grid.HorizontalOptions = LayoutOptions.Fill;

    //        grid.Children.Add(badge);
    //        stackLayout.Add(grid);
    //    }

    //    string? senderName = json.TryGetProperty("SenderName", out JsonElement senderNameElement) ? senderNameElement.GetString() : string.Empty;
    //    if (!string.IsNullOrEmpty(senderName))
    //    {
    //        stackLayout.Add(GenerateLabel($"From {senderName}", FontSizeOption.Description, TextAlignment.Start, Colors.White, null, new Thickness(0, 0, 0, 0)));
    //    }

    //    string? employName = json.TryGetProperty("EmployName", out JsonElement employNameElement) ? employNameElement.GetString() : string.Empty;
    //    if (!string.IsNullOrEmpty(senderName))
    //    {
    //        stackLayout.Add(GenerateLabel($"To {employName}", FontSizeOption.Description, TextAlignment.Start, Colors.White));
    //    }

    //    string? details = json.TryGetProperty("Details", out JsonElement detailsElement) ? detailsElement.GetString() : string.Empty;
    //    if (!string.IsNullOrEmpty(details))
    //    {
    //        stackLayout.Add(GenerateLabel(details, FontSizeOption.Medium, TextAlignment.Start, Colors.White));
    //    }

    //    string? taskTypeName = json.TryGetProperty("TaskTypeName", out JsonElement taskTypeNameElement) ? taskTypeNameElement.GetString() : string.Empty;
    //    if (!string.IsNullOrEmpty(taskTypeName))
    //    {
    //        stackLayout.Add(GenerateLabel(taskTypeName, FontSizeOption.Description, TextAlignment.Start, Colors.White));
    //    }

    //    border.Content = stackLayout;
    //    DynamicContents.Add(border);
    //}

    private void GenerateDetailElements(JsonElement json)
    {
        DynamicContents.Clear();

        //string? addedDate = json.TryGetProperty("AddedDate", out JsonElement addedDateElement) ? addedDateElement.GetString() : string.Empty;
        //string? endDate = json.TryGetProperty("EndDate", out JsonElement endDateElement) ? endDateElement.GetString() : string.Empty;
        //string? lastUpdateDate = json.TryGetProperty("LastUpdatedDate", out JsonElement lastUpdateDateElement) ? lastUpdateDateElement.GetString() : string.Empty;
        //string? taskStateId = json.TryGetProperty("TaskStateId", out JsonElement taskStateIdElement) ? taskStateIdElement.GetString() : string.Empty;
        //string? taskTypeId = json.TryGetProperty("TaskTypeId", out JsonElement taskTypeIdElement) ? taskTypeIdElement.GetString() : string.Empty;
        //string? senderUserImage = json.TryGetProperty("SenderUserImage", out JsonElement senderUserImageElement) ? senderUserImageElement.GetString() : string.Empty;
        //string? employUserImage = json.TryGetProperty("EmployUserImage", out JsonElement employUserImageElement) ? employUserImageElement.GetString() : string.Empty;
        //string? isRead = json.TryGetProperty("IsRead", out JsonElement isReadElement) ? isReadElement.GetString() : string.Empty;
        //string? messageCount = json.TryGetProperty("MessageCount", out JsonElement messageCountElement) ? messageCountElement.GetString() : string.Empty;
        //string? formData = json.TryGetProperty("FromData", out JsonElement formDataElement) ? formDataElement.GetString() : string.Empty;
        //string? messages = json.TryGetProperty("Messages", out JsonElement messagesElement) ? messagesElement.GetString() : string.Empty;

        string? title = json.TryGetProperty("Title", out JsonElement titleElement) ? titleElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(title))
        {
            Title = title;
        }

        var border = new Border();
        if (App.Current != null)
        {
            if (App.Current.Resources.TryGetValue("PrimaryDark", out var primaryDark) && primaryDark is Color primaryDarkColor)
            {
                border.Background = new SolidColorBrush(primaryDarkColor);
            }

            border.Stroke = new SolidColorBrush(Colors.Transparent);
            border.StrokeThickness = 0;
            border.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(0, 0, 30, 30)
            };

            border.Padding = new Thickness(20);
        }
        var stackLayout = new StackLayout();
        stackLayout.Padding = new Thickness(0);
        stackLayout.Margin = new Thickness(0);
        stackLayout.HorizontalOptions = LayoutOptions.Fill;

        // badge for task state
        string? taskStateName = json.TryGetProperty("TaskStateName", out JsonElement taskStateNameElement) ? taskStateNameElement.GetString() : string.Empty;
        string? color = json.TryGetProperty("Color", out JsonElement colorElement) ? colorElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(taskStateName) && !string.IsNullOrEmpty(color))
        {
            var grid = new Grid
            {
                ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Star } },
                RowDefinitions = { new RowDefinition { Height = GridLength.Auto } }
            };

            var badge = GenerateBadge(taskStateName, color);

            Grid.SetRow(badge, 0);
            Grid.SetColumn(badge, 0);
            grid.HorizontalOptions = LayoutOptions.Fill;

            grid.Children.Add(badge);
            stackLayout.Add(grid);
        }

        string? senderName = json.TryGetProperty("SenderName", out JsonElement senderNameElement) ? senderNameElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(senderName))
        {
            stackLayout.Add(GenerateLabel($"From {senderName}", FontSizeOption.Description, TextAlignment.Start, Colors.White, null, new Thickness(0, 0, 0, 0)));
        }

        string? employName = json.TryGetProperty("EmployName", out JsonElement employNameElement) ? employNameElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(senderName))
        {
            stackLayout.Add(GenerateLabel($"To {employName}", FontSizeOption.Description, TextAlignment.Start, Colors.White));
        }

        string? details = json.TryGetProperty("Details", out JsonElement detailsElement) ? detailsElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(details))
        {
            stackLayout.Add(GenerateLabel(details, FontSizeOption.Medium, TextAlignment.Start, Colors.White));
        }

        string? taskTypeName = json.TryGetProperty("TaskTypeName", out JsonElement taskTypeNameElement) ? taskTypeNameElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(taskTypeName))
        {
            stackLayout.Add(GenerateLabel(taskTypeName, FontSizeOption.Description, TextAlignment.Start, Colors.White));
        }

        border.Content = stackLayout;
        DynamicContents.Add(border);
    }

    private View GenerateLabel(string text, FontSizeOption? fontSize = null, TextAlignment? alignment = null, Color? textColor = null, string? id = null, Thickness? margin = null)
    {
        var parts = text.Split("<br />", StringSplitOptions.None);
        var color = textColor ?? Colors.Black;
        var size = (double)(fontSize ?? FontSizeOption.Medium);
        var align = alignment ?? TextAlignment.Start;
        var labelMargin = margin ?? new Thickness(0, 0, 0, 20);

        if (parts.Length == 1)
        {
            return new Label
            {
                Text = parts[0],
                FontAttributes = FontAttributes.Bold,
                FontSize = size,
                TextColor = color,
                HorizontalTextAlignment = align,
                Margin = labelMargin,
                StyleId = id
            };
        }

        var layout = new VerticalStackLayout { Spacing = 5 };
        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i].Trim();
            if (string.IsNullOrEmpty(part)) continue;

            layout.Children.Add(new Label
            {
                Text = part,
                FontAttributes = FontAttributes.Bold,
                FontSize = size,
                TextColor = color,
                HorizontalTextAlignment = align,
                Margin = margin ?? new Thickness(0, 0, 0, 5),
                StyleId = !string.IsNullOrEmpty(id) ? $"{id}_{i}" : null
            });
        }

        return layout;
    }

    private Color GetPrimaryDarkOrDefault(Color fallback)
    {
        if (App.Current?.Resources.TryGetValue("PrimaryDark", out var value) == true && value is Color primaryDark)
        {
            return primaryDark;
        }

        return fallback;
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
