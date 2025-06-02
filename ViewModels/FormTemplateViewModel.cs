using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;
using OfficeAnywhere.Mobile.Views;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Input;
using UraniumUI.Material.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace OfficeAnywhere.Mobile.ViewModels;

public class DynamicField
{
    public string FieldType { get; set; } = string.Empty; // Type of the field (e.g., TextField, Picker, etc.)
    public string Key { get; set; } = string.Empty; // Unique identifier for the field
    public string Label { get; set; } = string.Empty; // Display label
    public string Value { get; set; } = string.Empty; // Current value
    public List<string> Options { get; set; } = new(); // For Picker fields
}

public class PickerItem
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public partial class FormTemplateViewModel : ObservableObject
{
    private readonly FormTemplateService _formTemplateService;
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    private ObservableCollection<View> dynamicContents = new();

    [ObservableProperty]
    private bool isBusy = false;

    [ObservableProperty]
    private string? profilePicture;

    [ObservableProperty]
    private string? buttonText = "Submit";

    [ObservableProperty]
    private ObservableCollection<Form> taskType = new();

    [ObservableProperty]
    private Form? selectedTaskType = null;

    [ObservableProperty]
    private ObservableCollection<UserModelV2> users = new();

    [ObservableProperty]
    private UserModelV2? selectedUser = null;

    public FormTemplateViewModel(FormTemplateService formTemplateService, HttpClient httpClient)
    {
        _formTemplateService = formTemplateService;
        _httpClient = httpClient;
        InitializeAsync();
    }

    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("//TaskPage", true);
    }

    private async void InitializeAsync()
    {
        await FetchSelectionData();
    }

    [RelayCommand]
    public async Task FetchSelectionData()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            // Task Type
            List<Form>? taskType = await _formTemplateService.FetchTaskType();
            TaskType.Clear();
            TaskType = new ObservableCollection<Form>(taskType ?? []);

            // Users
            List<UserModelV2>? users = await _formTemplateService.FetchUsers();
            if (users != null && users.Count > 0)
            {
                Users.Clear();
                Users = new ObservableCollection<UserModelV2>(users);
            }
        }
        catch (Exception ex)
        {
            await Snackbar.Make(
                $"Error fetching API: {ex.Message}",
                duration: TimeSpan.FromSeconds(5),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = Colors.DarkRed,
                    TextColor = Colors.White,
                    ActionButtonTextColor = Colors.White,
                    CornerRadius = 8,
                    Font = Microsoft.Maui.Font.Default
                }
            ).Show();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            // Ensure required fields are selected
            //if (SelectedTaskType == null || SelectedUser == null)
            //{
            //    await Shell.Current.DisplayAlert("Error", "Please select a task type and user.", "OK");
            //    return;
            //}

            // Collect form data from dynamic components
            //var formData = new Dictionary<string, object>();
            //foreach (var component in DynamicContents)
            //{
            //    if (!string.IsNullOrEmpty(component.StyleId))
            //    {
            //        switch (component)
            //        {
            //            case TextField textField:
            //                formData[component.StyleId] = textField.Text ?? string.Empty;
            //                break;
            //            case EditorField editorField:
            //                formData[component.StyleId] = editorField.Text ?? string.Empty;
            //                break;
            //            case PickerField pickerField:
            //                formData[component.StyleId] = pickerField.SelectedItem?.ToString() ?? string.Empty;
            //                break;
            //            case DatePickerField datePickerField:
            //                formData[component.StyleId] = datePickerField.Date.Value.ToString("yyyy-MM-dd hh:mm a"); // ISO 8601 format
            //                break;
            //            case RadioButtonGroupView radioGroup:
            //                var selectedRadio = radioGroup.Children
            //                    .OfType<UraniumUI.Material.Controls.RadioButton>()
            //                    .FirstOrDefault(r => r.IsChecked);
            //                formData[component.StyleId] = selectedRadio?.Value?.ToString() ?? string.Empty;
            //                break;
            //        }
            //    }
            //}
            //formData["submit"] = true;

            //// Create the JSON payload
            //var payload = new
            //{
            //    Title = SelectedTaskType?.Name ?? "طلب تصميم",
            //    Details = string.Empty,
            //    EndDate = DateTime.UtcNow.ToString("o"), // Current UTC time in ISO 8601 format
            //    TaskStateId = 1,
            //    SenderId = 1, // Hardcoded as per example; adjust based on actual sender
            //    EmployId = SelectedUser?.Id ?? 2, // Use selected user's ID
            //    IsRefues = false, // Typo in example, keeping as is
            //    FormOrder = new
            //    {
            //        Id = 0,
            //        FormId = SelectedTaskType?.Id,
            //        UserName = SelectedUser?.UserName ?? "AymanOrg",
            //        AddedDate = DateTime.UtcNow.ToString("o"),
            //        FromData = JsonSerializer.Serialize(formData), // Serialize form data
            //        MetaData = string.Empty
            //    },
            //    TaskTypeId = SelectedTaskType?.Id,
            //    AddedDate = DateTime.UtcNow.ToString("o")
            //};

            // Send the payload to the server (example endpoint)
            //var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync("https://your-api-endpoint.com/submit", content); // Replace with actual endpoint
            //response.EnsureSuccessStatusCode();

            IsBusy = false;
            await Shell.Current.DisplayAlert("Success", "Form submitted successfully.", "OK");
            await Shell.Current.GoToAsync("//TaskPage", true);
        }
        catch (HttpRequestException ex)
        {
            IsBusy = false;
            await Shell.Current.DisplayAlert("Network Error", "Unable to connect to the server. Please check your internet connection.", "OK");
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await Shell.Current.DisplayAlert("Error", $"Submission failed: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnSelectedUserChanged(UserModelV2? value)
    {
        if (value != null)
        {
            // Handle the selected user
            // For example, display the selected user's ID or name
            Console.WriteLine($"Selected User: {value.UserName}, ID: {value.Id}");
            // You can add logic here, e.g., update UI, trigger actions, etc.
        }
        else
        {
            Console.WriteLine("No user selected.");
        }
    }

    partial void OnSelectedTaskTypeChanged(Form? oldValue, Form? newValue)
    {
        if (newValue != null)
        {
            DynamicContents.Clear();

            using JsonDocument document = JsonDocument.Parse(newValue.FormTemplate.Template);
            JsonElement root = document.RootElement;

            if (root.TryGetProperty("components", out JsonElement componentsArray) && componentsArray.ValueKind == JsonValueKind.Array)
            {
                processComponents(componentsArray);
            }
        }
        else
        {
            Console.WriteLine("No form selected.");
        }
    }

    private void processComponents(JsonElement componentsArray)
    {
        try
        {
            foreach (JsonElement component in componentsArray.EnumerateArray())
            {
                string? label = component.TryGetProperty("label", out JsonElement labelElement) ? labelElement.GetString() : string.Empty;
                string? legend = component.TryGetProperty("legend", out JsonElement legendElement) ? legendElement.GetString() : string.Empty;
                string? description = component.TryGetProperty("description", out JsonElement descElement) ? descElement.GetString() : string.Empty;
                string? content = component.TryGetProperty("content", out JsonElement contentElement) ? contentElement.GetString() : string.Empty;
                string? type = component.TryGetProperty("type", out JsonElement typeElement) ? typeElement.GetString() : string.Empty;
                string? id = component.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : string.Empty;
                string? key = component.TryGetProperty("key", out JsonElement keyElement) ? keyElement.GetString() : string.Empty;
                string? inputType = component.TryGetProperty("inputType", out JsonElement inputTypeElement) ? inputTypeElement.GetString() : string.Empty;

                if (type?.ToLower() == "htmlelement")
                {
                    if (!string.IsNullOrEmpty(content))
                    {
                        DynamicContents.Add(CreateLabel(content, id, FontSizeOption.Large));
                    }
                }

                if (type?.ToLower() == "datagrid")
                {
                    DynamicContents.Add(GenerateDataGrid(component));
                }

                if (type?.ToLower() == "fieldset")
                {
                    if (!string.IsNullOrEmpty(legend))
                    {
                        DynamicContents.Add(CreateLabel(legend, id, FontSizeOption.Medium));
                    }

                    if (component.TryGetProperty("components", out JsonElement fieldSetComponents))
                    {
                        processComponents(fieldSetComponents);
                    }
                }

                if (type?.ToLower() == "datetime")
                {
                    if (!string.IsNullOrEmpty(label))
                    {
                        DynamicContents.Add(CreateDatePickerField(label, id));
                    }
                }

                if (type?.ToLower() == "textfield")
                {
                    DynamicContents.Add(CreateTextField(label, key));
                }

                if (type?.ToLower() == "textarea")
                {
                    DynamicContents.Add(CreateEditorField(label, key));

                    if (component.TryGetProperty("components", out JsonElement fieldSetComponents))
                    {
                        processComponents(fieldSetComponents);
                    }
                }

                if (type?.ToLower() == "radio")
                {
                    if (!string.IsNullOrEmpty(label))
                    {
                        DynamicContents.Add(CreateLabel(label, id, FontSizeOption.Medium));
                    }

                    if (component.TryGetProperty("values", out JsonElement values) && values.ValueKind == JsonValueKind.Array)
                    {
                        List<RadioButtonOption> options = [];

                        foreach (JsonElement row in values.EnumerateArray())
                        {
                            row.TryGetProperty("label", out JsonElement labElement);
                            row.TryGetProperty("value", out JsonElement valueElement);
                            row.TryGetProperty("shortcut", out JsonElement shortcutElement);

                            string labelString = labElement.GetString() ?? string.Empty;
                            string valueString = valueElement.GetString() ?? string.Empty;
                            string shortcutString = shortcutElement.GetString() ?? string.Empty;

                            options.Add(new RadioButtonOption
                            {
                                Label = labelString,
                                Value = valueString,
                                Shortcut = shortcutString
                            });

                        }
                        DynamicContents.Add(createRadioButtonGroup(options));
                    }
                }

                if (type == "select")
                {
                    List<SelectOption> options = new();
                    bool isRequired = component.TryGetProperty("validate", out JsonElement validateElement) && validateElement.TryGetProperty("required", out JsonElement requiredElement) && requiredElement.GetBoolean();

                    if (component.TryGetProperty("dataSrc", out JsonElement dataSrcElement) && dataSrcElement.GetString() == "url")
                    {
                        if (component.TryGetProperty("data", out JsonElement dataElement) && dataElement.TryGetProperty("url", out JsonElement urlElement))
                        {
                            string url = urlElement.GetString() ?? string.Empty;
                            Dictionary<string, string> headers = new();
                            if (dataElement.TryGetProperty("headers", out JsonElement headersElement) && headersElement.ValueKind == JsonValueKind.Array)
                            {
                                foreach (JsonElement header in headersElement.EnumerateArray())
                                {
                                    string headerKey = header.TryGetProperty("key", out JsonElement keyEl) ? keyEl.GetString() ?? string.Empty : string.Empty;
                                    string headerValue = header.TryGetProperty("value", out JsonElement valueEl) ? valueEl.GetString() ?? string.Empty : string.Empty;
                                    if (!string.IsNullOrEmpty(headerKey) && !string.IsNullOrEmpty(headerValue))
                                    {
                                        headers.Add(headerKey, headerValue);
                                    }
                                }
                            }

                            string valueProperty = component.TryGetProperty("valueProperty", out JsonElement valuePropElement) ? valuePropElement.GetString() ?? "Id" : "Id";
                            string displayProperty = component.TryGetProperty("template", out JsonElement templateElement) && templateElement.GetString()?.Contains("item.Name") == true ? "Name" : "value";

                            try
                            {
                                var request = new HttpRequestMessage(HttpMethod.Get, url);
                                string accessToken = SecureStorage.GetAsync("AccessToken").Result ?? "";
                                string tenant = SecureStorage.GetAsync("Tenant").Result ?? "";
                                foreach (var header in headers)
                                {
                                    string value = header.Key.ToLower() == "tenant" ? tenant : header.Value;
                                    request.Headers.Add(header.Key, value);
                                }
                                request.Headers.Add("Authentication", $"Bearer {accessToken}");

                                var response = _httpClient.SendAsync(request).Result;
                                response.EnsureSuccessStatusCode();
                                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                                JsonElement root = doc.RootElement;

                                if (root.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (JsonElement item in root.EnumerateArray())
                                    {
                                        string value = item.TryGetProperty(valueProperty, out JsonElement valEl) ? valEl.ToString() : string.Empty;
                                        string display = item.TryGetProperty(displayProperty, out JsonElement dispEl) ? dispEl.GetString() ?? value : value;
                                        if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(display))
                                        {
                                            options.Add(new SelectOption { Value = value, Display = display });
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error fetching select options from {url}: {ex.Message}");
                            }
                        }
                    }
                    else if (component.TryGetProperty("data", out JsonElement dataElement) && dataElement.TryGetProperty("values", out JsonElement valuesElement) && valuesElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement value in valuesElement.EnumerateArray())
                        {
                            if (value.TryGetProperty("label", out JsonElement labelValue) && value.TryGetProperty("value", out JsonElement valueValue))
                            {
                                string display = labelValue.GetString() ?? string.Empty;
                                string val = valueValue.GetString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(display) && !string.IsNullOrEmpty(val))
                                {
                                    options.Add(new SelectOption { Display = display, Value = val });
                                }
                            }
                        }
                    }

                    var picker = CreatePickerField(label, id, options.Select(o => o.Display), options.FirstOrDefault()?.Display);
                    picker.StyleId = key;
                    DynamicContents.Add(picker);
                }

                if (type?.ToLower() == "button" && key?.ToLower() == "submit")
                {
                    ButtonText = label ?? "Submit";
                }

                if (type?.ToLower() == "table")
                {
                    if (component.TryGetProperty("rows", out JsonElement rowsElement) && component.TryGetProperty("numRows", out JsonElement numRowsElement))
                    {
                        int numRows = numRowsElement.GetInt32();

                        foreach (JsonElement row in rowsElement.EnumerateArray())
                        {
                            foreach (JsonElement cell in row.EnumerateArray())
                            {
                                if (cell.TryGetProperty("components", out JsonElement cellComponents))
                                {
                                    processComponents(cellComponents);
                                }
                            }
                        }
                    }
                }

                if (component.TryGetProperty("table", out JsonElement tableElement))
                {
                    if (component.TryGetProperty("rows", out JsonElement rowsElement) && component.TryGetProperty("numRows", out JsonElement numRowsElement))
                    {
                        int numRows = numRowsElement.GetInt32();

                        foreach (JsonElement row in rowsElement.EnumerateArray())
                        {
                            foreach (JsonElement cell in row.EnumerateArray())
                            {
                                if (cell.TryGetProperty("components", out JsonElement cellComponents))
                                {
                                    processComponents(cellComponents);
                                }
                            }
                        }
                    }
                }
                //if (component.TryGetProperty("components", out JsonElement subComponents) && subComponents.ValueKind == JsonValueKind.Array)
                //{
                //    processComponents(subComponents);
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }

    //private View generateDataGrid(JsonElement dataGridComponent)
    //{
    //    StackLayout stackLayoutParent = new StackLayout
    //    {
    //        HorizontalOptions = LayoutOptions.Fill,
    //        VerticalOptions = LayoutOptions.Start,
    //        Margin = new Thickness(0, 0, 0, 20)
    //    };

    //    ScrollView scrollView = new ScrollView
    //    {
    //        Orientation = ScrollOrientation.Horizontal,
    //        HorizontalOptions = LayoutOptions.Fill,
    //        Margin = new Thickness(0, 0, 0, 20)
    //    };

    //    try
    //    {
    //        string? dataGridKey = dataGridComponent.TryGetProperty("key", out JsonElement dataGridKeyElement) ? dataGridKeyElement.GetString() : string.Empty;
    //        string? label = dataGridComponent.TryGetProperty("label", out JsonElement labelElement) ? labelElement.GetString() : string.Empty;
    //        string? legend = dataGridComponent.TryGetProperty("legend", out JsonElement legendElement) ? legendElement.GetString() : string.Empty;
    //        string? description = dataGridComponent.TryGetProperty("description", out JsonElement descElement) ? descElement.GetString() : string.Empty;
    //        string? addAnother = dataGridComponent.TryGetProperty("addAnother", out JsonElement addAnotherElement) ? addAnotherElement.GetString() : string.Empty;
    //        string? content = dataGridComponent.TryGetProperty("content", out JsonElement contentElement) ? contentElement.GetString() : string.Empty;

    //        Grid dataGrid = new Grid();
    //        dataGrid.ColumnSpacing = 0;
    //        dataGrid.RowSpacing = 0;

    //        if (dataGridComponent.TryGetProperty("components", out JsonElement gridElements) &&
    //            gridElements.ValueKind == JsonValueKind.Array)
    //        {
    //            int columnCount = gridElements.GetArrayLength();

    //            // Define initial rows: headers, one input row, and button row
    //            dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Header row
    //            dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Initial input row
    //            dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Button row

    //            // Define one column per component
    //            for (int i = 0; i < columnCount; i++)
    //            {
    //                dataGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
    //            }

    //            int columnIndex = 0;

    //            // First row: headers with borders
    //            foreach (JsonElement element in gridElements.EnumerateArray())
    //            {
    //                string? id = element.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : string.Empty;
    //                string? header = element.TryGetProperty("label", out JsonElement headerElement) ? headerElement.GetString() : string.Empty;

    //                var cellLabel = new Label
    //                {
    //                    Text = header,
    //                    StyleId = id,
    //                    FontSize = (double)FontSizeOption.Description,
    //                    FontAttributes = FontAttributes.Bold,
    //                    HorizontalOptions = LayoutOptions.Center,
    //                    VerticalOptions = LayoutOptions.Center,
    //                    HorizontalTextAlignment = TextAlignment.Center,
    //                    VerticalTextAlignment = TextAlignment.Center,
    //                    Padding = new Thickness(10)
    //                };

    //                var cellFrame = new Frame
    //                {
    //                    Content = cellLabel,
    //                    BorderColor = Colors.Gray,
    //                    Padding = 0,
    //                    CornerRadius = 0,
    //                    Margin = 0,
    //                    HasShadow = false
    //                };

    //                Grid.SetRow(cellFrame, 0);
    //                Grid.SetColumn(cellFrame, columnIndex);
    //                dataGrid.Children.Add(cellFrame);

    //                columnIndex++;
    //            }

    //            columnIndex = 0;

    //            // Second row: initial input fields with borders
    //            foreach (JsonElement element in gridElements.EnumerateArray())
    //            {
    //                string? key = element.TryGetProperty("key", out JsonElement keyElement) ? keyElement.GetString() : string.Empty;

    //                var entry = new TextField
    //                {
    //                    StyleId = $"data[{dataGridKey}][{(rowCount - 2)}][{key}]",
    //                    FontSize = (double)FontSizeOption.Medium,
    //                    Keyboard = Keyboard.Text,
    //                    WidthRequest = 200,
    //                    HorizontalOptions = LayoutOptions.Fill,
    //                    VerticalOptions = LayoutOptions.Center,
    //                    Margin = new Thickness(5, 0)
    //                };

    //                var cellFrame = new Frame
    //                {
    //                    Content = entry,
    //                    BorderColor = Colors.Gray,
    //                    Padding = 5,
    //                    CornerRadius = 0,
    //                    Margin = 0,
    //                    HasShadow = false
    //                };

    //                Grid.SetRow(cellFrame, 1);
    //                Grid.SetColumn(cellFrame, columnIndex);
    //                dataGrid.Children.Add(cellFrame);

    //                columnIndex++;
    //            }


    //            // Add "Add Another" button in the last row, spanning all columns
    //            var addButton = new Button
    //            {
    //                Text = addAnother ?? "Add Another",
    //                HorizontalOptions = LayoutOptions.Start,
    //                VerticalOptions = LayoutOptions.Start,
    //                Margin = new Thickness(10)
    //            };

    //            // Safely access resources
    //            if (App.Current.Resources.TryGetValue("PrimaryDark", out var primaryDark) && primaryDark is Color primaryDarkColor)
    //            {
    //                addButton.BackgroundColor = primaryDarkColor;
    //            }
    //            else
    //            {
    //                addButton.BackgroundColor = Colors.DarkBlue;
    //            }

    //            if (App.Current.Resources.TryGetValue("White", out var white) && white is Color whiteColor)
    //            {
    //                addButton.TextColor = whiteColor;
    //            }
    //            else
    //            {
    //                addButton.TextColor = Colors.White;
    //            }

    //            var buttonFrame = new Frame
    //            {
    //                Content = addButton,
    //                BorderColor = Colors.Gray,
    //                Padding = 5,
    //                CornerRadius = 0,
    //                Margin = 0,
    //                HasShadow = false
    //            };

    //            Grid.SetRow(buttonFrame, 2);
    //            Grid.SetColumnSpan(buttonFrame, columnCount);
    //            dataGrid.Children.Add(buttonFrame);

    //            // Handle button click to add new input row
    //            int rowCount = 2; // Start with 2 rows (header + first input row)
    //            addButton.Clicked += (sender, args) =>
    //            {
    //                // Remove button from current row
    //                dataGrid.Children.Remove(buttonFrame);
    //                dataGrid.RowDefinitions.RemoveAt(rowCount); // Remove button row

    //                // Add new input row
    //                rowCount++;
    //                dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // New input row

    //                columnIndex = 0;
    //                foreach (JsonElement element in gridElements.EnumerateArray())
    //                {
    //                    string? key = element.TryGetProperty("key", out JsonElement keyElement) ? keyElement.GetString() : string.Empty;

    //                    var entry = new TextField
    //                    {
    //                        StyleId = $"data[{dataGridKey}][{(rowCount - 2)}][{key}]",
    //                        FontSize = (double)FontSizeOption.Medium,
    //                        Keyboard = Keyboard.Text,
    //                        WidthRequest = 200,
    //                        HorizontalOptions = LayoutOptions.Fill,
    //                        VerticalOptions = LayoutOptions.Center,
    //                        Margin = new Thickness(5, 0)
    //                    };

    //                    var cellFrame = new Frame
    //                    {
    //                        Content = entry,
    //                        BorderColor = Colors.Gray,
    //                        Padding = 5,
    //                        CornerRadius = 0,
    //                        Margin = 0,
    //                        HasShadow = false
    //                    };

    //                    Grid.SetRow(cellFrame, rowCount - 1);
    //                    Grid.SetColumn(cellFrame, columnIndex);
    //                    dataGrid.Children.Add(cellFrame);

    //                    columnIndex++;
    //                }

    //                // Add button row back at the end
    //                dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
    //                Grid.SetRow(buttonFrame, rowCount);
    //                dataGrid.Children.Add(buttonFrame);
    //            };

    //            scrollView.Content = dataGrid;
    //            stackLayoutParent.Children.Add(scrollView);

    //            if (!string.IsNullOrEmpty(description))
    //            {
    //                string? id = dataGridComponent.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : string.Empty;

    //                var labelComponent = new Label
    //                {
    //                    Text = description,
    //                    IsVisible = true,
    //                    StyleId = id,
    //                    FontSize = (double)FontSizeOption.Description,
    //                    FontAttributes = FontAttributes.Bold,
    //                    Margin = new Thickness(0, 0, 0, 20)
    //                };
    //                stackLayoutParent.Children.Add(labelComponent);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("Error in generateDataGrid: " + ex.Message);
    //    }

    //    return stackLayoutParent;
    //}

    // Dictionary to store gridElements for each data grid, keyed by dataGridKey
    private static readonly Dictionary<string, JsonElement> GridElementsCache = new Dictionary<string, JsonElement>();

    private View GenerateDataGrid(JsonElement dataGridComponent)
    {
        StackLayout stackLayoutParent = new StackLayout
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, 0, 0, 20)
        };

        ScrollView scrollView = new ScrollView
        {
            Orientation = ScrollOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(0, 0, 0, 20)
        };

        try
        {
            string? dataGridKey = dataGridComponent.TryGetProperty("key", out JsonElement dataGridKeyElement) ? dataGridKeyElement.GetString() : string.Empty;
            string? label = dataGridComponent.TryGetProperty("label", out JsonElement labelElement) ? labelElement.GetString() : string.Empty;
            string? legend = dataGridComponent.TryGetProperty("legend", out JsonElement legendElement) ? legendElement.GetString() : string.Empty;
            string? description = dataGridComponent.TryGetProperty("description", out JsonElement descElement) ? descElement.GetString() : string.Empty;
            string? addAnother = dataGridComponent.TryGetProperty("addAnother", out JsonElement addAnotherElement) ? addAnotherElement.GetString() : "Add Another";
            string? content = dataGridComponent.TryGetProperty("content", out JsonElement contentElement) ? contentElement.GetString() : string.Empty;

            Grid dataGrid = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0
            };

            if (dataGridComponent.TryGetProperty("components", out JsonElement gridElements) &&
                gridElements.ValueKind == JsonValueKind.Array)
            {
                // Store a clone of gridElements to prevent disposal issues
                if (!string.IsNullOrEmpty(dataGridKey))
                {
                    GridElementsCache[dataGridKey] = gridElements.Clone();
                }

                int columnCount = gridElements.GetArrayLength();

                // Define initial rows: headers, one input row, and button row
                dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Header row
                dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Initial input row
                dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Button row

                // Define one column per component
                for (int i = 0; i < columnCount; i++)
                {
                    dataGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                }

                int columnIndex = 0;

                // First row: headers with borders
                foreach (JsonElement element in gridElements.EnumerateArray())
                {
                    string? id = element.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : string.Empty;
                    string? header = element.TryGetProperty("label", out JsonElement headerElement) ? headerElement.GetString() : string.Empty;

                    var cellLabel = new Label
                    {
                        Text = header,
                        StyleId = id,
                        FontSize = (double)FontSizeOption.Description,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        Padding = new Thickness(10)
                    };

                    var cellFrame = new Frame
                    {
                        Content = cellLabel,
                        BorderColor = Colors.Gray,
                        Padding = 0,
                        CornerRadius = 0,
                        Margin = 0,
                        HasShadow = false
                    };

                    Grid.SetRow(cellFrame, 0);
                    Grid.SetColumn(cellFrame, columnIndex);
                    dataGrid.Children.Add(cellFrame);

                    columnIndex++;
                }

                // Second row: initial input fields
                int rowCount = 2; // Start with 2 rows (header + first input row)
                AddInputRow(dataGrid, GridElementsCache[dataGridKey], dataGridKey, rowCount - 1);

                // Add "Add Another" button in the last row, spanning all columns
                var addButton = new Button
                {
                    Text = addAnother ?? "Add Another",
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(10)
                };

                // Safely access resources
                if (App.Current.Resources.TryGetValue("PrimaryDark", out var primaryDark) && primaryDark is Color primaryDarkColor)
                {
                    addButton.BackgroundColor = primaryDarkColor;
                }
                else
                {
                    addButton.BackgroundColor = Colors.DarkBlue;
                }

                if (App.Current.Resources.TryGetValue("White", out var white) && white is Color whiteColor)
                {
                    addButton.TextColor = whiteColor;
                }
                else
                {
                    addButton.TextColor = Colors.White;
                }

                var buttonFrame = new Frame
                {
                    Content = addButton,
                    BorderColor = Colors.Gray,
                    Padding = 5,
                    CornerRadius = 0,
                    Margin = 0,
                    HasShadow = false
                };

                Grid.SetRow(buttonFrame, rowCount);
                Grid.SetColumnSpan(buttonFrame, columnCount);
                dataGrid.Children.Add(buttonFrame);

                // Handle button click to add new input row
                addButton.Clicked += (sender, args) =>
                {
                    // Remove button from current row
                    dataGrid.Children.Remove(buttonFrame);
                    dataGrid.RowDefinitions.RemoveAt(rowCount); // Remove button row

                    // Add new input row
                    rowCount++;
                    dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // New input row
                    AddInputRow(dataGrid, GridElementsCache[dataGridKey], dataGridKey, rowCount - 1);

                    // Add button row back at the end
                    dataGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Grid.SetRow(buttonFrame, rowCount);
                    dataGrid.Children.Add(buttonFrame);
                };

                scrollView.Content = dataGrid;
                stackLayoutParent.Children.Add(scrollView);

                if (!string.IsNullOrEmpty(description))
                {
                    string? id = dataGridComponent.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : string.Empty;

                    var labelComponent = new Label
                    {
                        Text = description,
                        IsVisible = true,
                        StyleId = id,
                        FontSize = (double)FontSizeOption.Description,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(0, 0, 0, 20)
                    };
                    stackLayoutParent.Children.Add(labelComponent);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in generateDataGrid: " + ex.Message);
        }

        return stackLayoutParent;
    }

    private View GenerateTextField(string styleId, string title = "", FontSizeOption fontSize = FontSizeOption.Medium,
                                LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null,
                                int widthRequest = 0)
    {
        var entry = new TextField
        {
            Title = title,
            StyleId = styleId,
            FontSize = (double)fontSize,
            Keyboard = Keyboard.Text,
            HorizontalOptions = horizontalOptions ?? LayoutOptions.Start,
            VerticalOptions = verticalOptions ?? LayoutOptions.Center,
            Margin = new Thickness(5, 0)
        };

        if (widthRequest > 0)
        {
            entry.WidthRequest = widthRequest;
        }

        return entry;
    }

    private View GenerateDatePickerField(string styleId, string title = "", FontSizeOption fontSize = FontSizeOption.Medium,
                                LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null,
                                int widthRequest = 0)
    {
        var datePicker = new DatePickerField
        {
            Title = title,
            StyleId = styleId,
            FontSize = (double)fontSize,
            HorizontalOptions = horizontalOptions ?? LayoutOptions.Start,
            VerticalOptions = verticalOptions ?? LayoutOptions.Center,
            Margin = new Thickness(5, 0)
        };

        if (widthRequest > 0)
        {
            datePicker.WidthRequest = widthRequest;
        }

        return datePicker;
    }

    private View GenerateEditorField(string styleId, string title = "", FontSizeOption fontSize = FontSizeOption.Medium,
                                LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null,
                                int widthRequest = 0)
    {
        var entry = new EditorField
        {
            Title = title,
            StyleId = styleId,
            FontSize = (double)fontSize,
            Keyboard = Keyboard.Text,
            HorizontalOptions = horizontalOptions ?? LayoutOptions.Start,
            VerticalOptions = verticalOptions ?? LayoutOptions.Center,
            Margin = new Thickness(5, 0)
        };

        if (widthRequest > 0)
        {
            entry.WidthRequest = widthRequest;
        }

        return entry;
    }

    private View GeneratePickerField(string styleId, string title = "", List<PickerItem> itemSource = null,
                                   string display = "Value",
                                   FontSizeOption fontSize = FontSizeOption.Medium,
                                   LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null,
                                   int widthRequest = 0)
    {
        var picker = new PickerField
        {
            Title = title,
            StyleId = styleId,
            FontSize = (double)fontSize,
            HorizontalOptions = horizontalOptions ?? LayoutOptions.Start,
            VerticalOptions = verticalOptions ?? LayoutOptions.Center,
            Margin = new Thickness(5, 0)
        };

        if (itemSource != null)
        {
            picker.ItemsSource = itemSource;
            picker.ItemDisplayBinding = new Binding(display);
        }

        if (widthRequest > 0)
        {
            picker.WidthRequest = widthRequest;
        }

        return picker;
    }

    private void AddInputRow(Grid dataGrid, JsonElement gridElements, string dataGridKey, int rowIndex)
    {
        int columnIndex = 0;
        foreach (JsonElement element in gridElements.EnumerateArray())
        {
            string? key = element.TryGetProperty("key", out JsonElement keyElement) ? keyElement.GetString() : string.Empty;

            var entry = GenerateTextField(
                styleId: $"data[{dataGridKey}][{rowIndex}][{key}]", 
                fontSize: FontSizeOption.Medium,
                horizontalOptions: LayoutOptions.Fill, 
                verticalOptions: LayoutOptions.Center, 
                widthRequest: 200);

            var cellFrame = new Frame
            {
                Content = entry,
                BorderColor = Colors.Gray,
                Padding = 5,
                CornerRadius = 0,
                Margin = 0,
                HasShadow = false
            };

            Grid.SetRow(cellFrame, rowIndex);
            Grid.SetColumn(cellFrame, columnIndex);
            dataGrid.Children.Add(cellFrame);

            columnIndex++;
        }
    }

    public class SelectOption
    {
        public string Value { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
    }

    private Label CreateLabel(string text, string id, FontSizeOption fontSize, Thickness? margin = null)
    {
        return new Label
        {
            Text = text,
            StyleId = id,
            FontSize = (double)fontSize,
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(0, 0, 0, 20)
        };
    }

    // Function to create a Button
    //private Button CreateButton(string text, string id, InputType? inputType = InputType.Text, Thickness? margin = null)
    //{
    //    return new Button
    //    {
    //        Text = text,
    //        StyleId = id,
    //        FontAttributes = FontAttributes.Bold,
    //        HeightRequest = 45,
    //        FontSize = (double)FontSizeOption.Medium,
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };
    //}

    // Function to create a TextField
    private TextField CreateTextField(string text, string id, InputType? inputType = InputType.Text, Thickness? margin = null)
    {
        return new TextField
        {
            Title = text,
            StyleId = id,
            FontSize = (double)FontSizeOption.Medium,
            Keyboard = Keyboard.Text,
            Margin = margin ?? new Thickness(0, 0, 0, 20)
        };
    }

    // Function to create a EditorField
    private EditorField CreateEditorField(string text, string id, InputType? inputType = InputType.Text, Thickness? margin = null)
    {
        return new EditorField
        {
            Title = text,
            StyleId = id,
            FontSize = (double)FontSizeOption.Medium,
            Keyboard = Keyboard.Text,
            Margin = margin ?? new Thickness(0, 0, 0, 20)
        };
    }

    // Function to create a PickerField
    private PickerField CreatePickerField(string title, string id, IEnumerable<string> items, string defaultSelection = null, Thickness? margin = null)
    {
        return new PickerField
        {
            Title = title,
            StyleId = id,
            ItemsSource = items.ToList(),
            SelectedItem = defaultSelection ?? items.FirstOrDefault(), // Default to first item if none specified
            Margin = margin ?? new Thickness(0, 0, 0, 20)
        };
    }

    // Function to create a DatePickerField
    private DatePickerField CreateDatePickerField(string title, string id, DateTime? defaultDate = null, Thickness? margin = null)
    {
        return new DatePickerField
        {
            Title = title,
            StyleId = id,
            FontSize = (double)FontSizeOption.Medium,
            Date = defaultDate ?? DateTime.Now,
            Margin = margin ?? new Thickness(0, 0, 0, 20)
        };
    }

    public class RadioButtonOption
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Shortcut { get; set; } = string.Empty;
    }
    private RadioButtonGroupView createRadioButtonGroup(List<RadioButtonOption> options, Thickness? margin = null)
    {
        RadioButtonGroupView radioButtonGroupView = new RadioButtonGroupView
        {
            Margin = margin ?? new Thickness(0, 0, 0, 20),
            //Orientation = StackOrientation.Horizontal,
        };

        int optionIndex = 0;
        foreach (RadioButtonOption option in options)
        {
            radioButtonGroupView.Children.Add(new UraniumUI.Material.Controls.RadioButton
            {
                Text = option.Label,
                Value = option.Value,
                TextFontSize = (double)FontSizeOption.Medium,
                IsChecked = optionIndex == 0
            });
            optionIndex++;
        }

        return radioButtonGroupView;
    }

    private RadioButtonGroupView CreateRadioButtonGroupView(string[] options, string[] values, Thickness? margin = null)
    {
        if (options.Length != values.Length)
        {
            throw new ArgumentException("Options and values arrays must have the same length.");
        }

        var radioButtonGroupView = new RadioButtonGroupView
        {
            Margin = margin ?? new Thickness(0, 0, 0, 20)
        };

        for (int i = 0; i < options.Length; i++)
        {
            radioButtonGroupView.Children.Add(new UraniumUI.Material.Controls.RadioButton
            {
                Text = options[i],
                Value = values[i],
                IsChecked = true
            });
        }

        return radioButtonGroupView;
    }
}