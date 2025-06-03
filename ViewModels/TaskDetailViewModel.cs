using CommunityToolkit.Mvvm.ComponentModel;
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
    private ObservableCollection<View> dynamicContents = new();

    public TaskDetailViewModel(DelmonTaskCacheService delmonTaskCacheService)
    {
        _delmonTaskCacheService = delmonTaskCacheService;
        DelmonTask = _delmonTaskCacheService.GetCachedTask();

        if (DelmonTask is not null)
        {
            using JsonDocument document = JsonDocument.Parse(DelmonTask);
            JsonElement root = document.RootElement;

            GenerateDetailElements(root);
        }
        else
        {
            ToListTask();
        }
    }

    private async void ToListTask() => await Shell.Current.GoToAsync("//TaskPage");

    private void GenerateDetailElements(JsonElement json)
    {
        string? addedDate = json.TryGetProperty("AddedDate", out JsonElement addedDateElement) ? addedDateElement.GetString() : string.Empty;
        string? endDate = json.TryGetProperty("EndDate", out JsonElement endDateElement) ? endDateElement.GetString() : string.Empty;
        string? lastUpdateDate = json.TryGetProperty("LastUpdatedDate", out JsonElement lastUpdateDateElement) ? lastUpdateDateElement.GetString() : string.Empty;
        string? taskStateId = json.TryGetProperty("TaskStateId", out JsonElement taskStateIdElement) ? taskStateIdElement.GetString() : string.Empty;
        string? taskTypeId = json.TryGetProperty("TaskTypeId", out JsonElement taskTypeIdElement) ? taskTypeIdElement.GetString() : string.Empty;
        string? taskStateName = json.TryGetProperty("TaskStateName", out JsonElement taskStateNameElement) ? taskStateNameElement.GetString() : string.Empty;
        string? taskTypeName = json.TryGetProperty("TaskTypeName", out JsonElement taskTypeNameElement) ? taskTypeNameElement.GetString() : string.Empty;
        string? senderUserImage = json.TryGetProperty("SenderUserImage", out JsonElement senderUserImageElement) ? senderUserImageElement.GetString() : string.Empty;
        string? employUserImage = json.TryGetProperty("EmployUserImage", out JsonElement employUserImageElement) ? employUserImageElement.GetString() : string.Empty;
        string? isRead = json.TryGetProperty("IsRead", out JsonElement isReadElement) ? isReadElement.GetString() : string.Empty;
        string? messageCount = json.TryGetProperty("MessageCount", out JsonElement messageCountElement) ? messageCountElement.GetString() : string.Empty;
        string? formData = json.TryGetProperty("FromData", out JsonElement formDataElement) ? formDataElement.GetString() : string.Empty;
        string? messages = json.TryGetProperty("Messages", out JsonElement messagesElement) ? messagesElement.GetString() : string.Empty;

        string? title = json.TryGetProperty("Title", out JsonElement titleElement) ? titleElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(title))
        {
            DynamicContents.Add(GenerateLabel(title, FontSizeOption.Large, TextAlignment.Center));
        }

        string? details = json.TryGetProperty("Details", out JsonElement detailsElement) ? detailsElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(details))
        {
            DynamicContents.Add(GenerateLabel(details, FontSizeOption.Medium));
        }

        string? senderName = json.TryGetProperty("SenderName", out JsonElement senderNameElement) ? senderNameElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(senderName))
        {
            DynamicContents.Add(GenerateLabel($"From: {senderName}", FontSizeOption.Medium));
        }

        string? employName = json.TryGetProperty("EmployName", out JsonElement employNameElement) ? employNameElement.GetString() : string.Empty;
        if (!string.IsNullOrEmpty(senderName))
        {
            DynamicContents.Add(GenerateLabel($"To: {employName}", FontSizeOption.Medium));
        }
    }

    private View GenerateLabel(string text, FontSizeOption? fontSize, TextAlignment? textAlignment = TextAlignment.Start, string? id = "", Thickness? margin = null)
    {
        var textParts = text.Split("//br", StringSplitOptions.None);

        if (textParts.Length == 1)
        {
            var singleLabel = new Label
            {
                Text = textParts[0],
                FontAttributes = FontAttributes.Bold,
                Margin = margin is not null ? margin.Value : new Thickness(0, 0, 0, 20),
                FontSize = fontSize is not null ? (double)fontSize : (double)FontSizeOption.Medium,
                HorizontalTextAlignment = textAlignment is not null ? textAlignment.Value : TextAlignment.Start
            };

            if (!string.IsNullOrEmpty(id))
            {
                singleLabel.StyleId = id;
            }

            return singleLabel;
        }

        var layout = new VerticalStackLayout
        {
            Spacing = 5
        };

        foreach (var part in textParts)
        {
            if (!string.IsNullOrEmpty(part))
            {
                var label = new Label
                {
                    Text = part.Trim(),
                    FontAttributes = FontAttributes.Bold,
                    Margin = margin is not null ? margin.Value : new Thickness(0, 0, 0, 5),
                    FontSize = fontSize is not null ? (double)fontSize : (double)FontSizeOption.Medium,
                    HorizontalTextAlignment = textAlignment is not null ? textAlignment.Value : TextAlignment.Start
                };

                if (!string.IsNullOrEmpty(id))
                {
                    label.StyleId = $"{id}_{layout.Children.Count}";
                }

                layout.Children.Add(label);
            }
        }

        return layout;
    }
}
