using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using UraniumUI.Material.Controls;

namespace OfficeAnywhere.Mobile.Views;

//public enum FontSizeOption
//{
//    Large = 28,
//    Medium = 22,
//    Description = 16
//}

//public enum InputType
//{
//    Text,
//    Email,
//    Numeric,
//    Telephone,
//    Url,
//    Chat
//}

//public static class InputTypeExtensions
//{
//    public static Keyboard ToKeyboard(this InputType inputType)
//    {
//        return inputType switch
//        {
//            InputType.Text => Keyboard.Text,
//            InputType.Email => Keyboard.Email,
//            InputType.Numeric => Keyboard.Numeric,
//            InputType.Telephone => Keyboard.Telephone,
//            InputType.Url => Keyboard.Url,
//            InputType.Chat => Keyboard.Chat,
//            _ => Keyboard.Default
//        };
//    }
//}

public partial class ExperimentPage : ContentPage
{
    //[RelayCommand]
    //private async Task SubmitAsync()
    //{
    //    //var data = CollectData();
    //    //Console.WriteLine(data);
    //}

    private readonly ExperimentViewModel viewModel;
    public ExperimentPage(ExperimentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;

        //string jsonString = getJson().Result;

        //FormLayout.Children.Clear();
        //foreach (var view in viewModel.DynamicContents)
        //{
        //    FormLayout.Children.Add(view);
        //}
    }

    //// Assuming these are your collections
    //private List<string> CompanyTypes { get; set; } = new List<string>();

    //// Initialize CompanyTypes (can be called once during setup)
    //private void InitializeCompanyTypes()
    //{
    //    CompanyTypes.AddRange(new[] { "Technology", "Finance", "Healthcare", "Manufacturing", "Retail" });
    //}

    //// Usage example: Adding fields to DynamicContents
    //private void SetupDynamicForm()
    //{
    //    // Initialize CompanyTypes
    //    //InitializeCompanyTypes();

    //    //// Add TextFields
    //    //viewModel.DynamicContents.Add(CreateTextField("Company Name"));
    //    //viewModel.DynamicContents.Add(CreateTextField("Company Age", Keyboard.Numeric));

    //    //// Add PickerField
    //    //viewModel.DynamicContents.Add(CreatePickerField("Company Types", CompanyTypes));

    //    //// Add DatePickerField
    //    //viewModel.DynamicContents.Add(CreateDatePickerField("Company Established"));

    //    //// Add RadioButtonGroupView
    //    //string[] radioOptions = { "Yes", "No", "Maybe Later" };
    //    //string[] radioValues = { "yes", "no", "my" };
    //    //viewModel.DynamicContents.Add(CreateRadioButtonGroupView(radioOptions, radioValues));
    //}

    //private Label CreateLabel(string text, string id, FontSizeOption fontSize, Thickness? margin = null)
    //{
    //    return new Label
    //    {
    //        Text = text,
    //        StyleId = id,
    //        FontSize = (double)fontSize,
    //        FontAttributes = FontAttributes.Bold,
    //        Margin = new Thickness(0, 0, 0, 20)
    //    };
    //}

    //// Function to create a Button
    //private Button CreateButton(string text, string id, InputType? inputType = InputType.Text, Thickness? margin = null)
    //{
    //    return new Button
    //    {
    //        Text = text,
    //        StyleId = id,
    //        FontSize = (double)FontSizeOption.Medium,
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };
    //}

    //// Function to create a TextField
    //private TextField CreateTextField(string text, string id, InputType? inputType = InputType.Text, Thickness? margin = null)
    //{
    //    return new TextField
    //    {
    //        Title = text,
    //        StyleId = id,
    //        FontSize = (double)FontSizeOption.Medium,
    //        Keyboard = Keyboard.Text,
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };
    //}

    //// Function to create a EditorField
    //private EditorField CreateEditorField(string text, string id, InputType? inputType = InputType.Text, Thickness? margin = null)
    //{
    //    return new EditorField
    //    {
    //        Title = text,
    //        StyleId = id,
    //        FontSize = (double)FontSizeOption.Medium,
    //        Keyboard = Keyboard.Text,
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };
    //}

    //// Function to create a PickerField
    //private PickerField CreatePickerField(string title, IEnumerable<string> items, string defaultSelection = null, Thickness? margin = null)
    //{
    //    return new PickerField
    //    {
    //        Title = title,
    //        ItemsSource = items.ToList(),
    //        SelectedItem = defaultSelection ?? items.FirstOrDefault(), // Default to first item if none specified
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };
    //}

    //// Function to create a DatePickerField
    //private DatePickerField CreateDatePickerField(string title, DateTime? defaultDate = null, Thickness? margin = null)
    //{
    //    return new DatePickerField
    //    {
    //        Title = title,
    //        Date = defaultDate ?? DateTime.Now, // Default to current date if none specified
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };
    //}

    //// Function to create a RadioButtonGroupView
    //private RadioButtonGroupView CreateRadioButtonGroupView(string[] options, string[] values, Thickness? margin = null)
    //{
    //    if (options.Length != values.Length)
    //    {
    //        throw new ArgumentException("Options and values arrays must have the same length.");
    //    }

    //    var radioButtonGroupView = new RadioButtonGroupView
    //    {
    //        Margin = margin ?? new Thickness(0, 0, 0, 20)
    //    };

    //    for (int i = 0; i < options.Length; i++)
    //    {
    //        radioButtonGroupView.Children.Add(new UraniumUI.Material.Controls.RadioButton
    //        {
    //            Text = options[i],
    //            Value = values[i]
    //        });
    //    }

    //    return radioButtonGroupView;
    //}

    //public async Task<string> getJson()
    //{
    //    try
    //    {
    //        using var stream = await FileSystem.OpenAppPackageFileAsync("FormTemplate.txt");
    //        using var reader = new StreamReader(stream);

    //        var contents = reader.ReadToEnd();

    //        using JsonDocument document = JsonDocument.Parse(contents);
    //        JsonElement root = document.RootElement;

    //        if (root.TryGetProperty("components", out JsonElement componentsArray) && componentsArray.ValueKind == JsonValueKind.Array)
    //        {
    //            processComponents(componentsArray);
    //        }

    //        return contents;
    //    }
    //    catch (Exception ex)
    //    {
    //        return string.Empty;
    //    }
    //}

    //private void processComponents(JsonElement componentsArray)
    //{
    //    try
    //    {
    //        foreach (JsonElement component in componentsArray.EnumerateArray())
    //        {
    //            string? label = component.TryGetProperty("label", out JsonElement labelElement) ? labelElement.GetString() : string.Empty;
    //            string? description = component.TryGetProperty("description", out JsonElement descElement) ? descElement.GetString() : string.Empty;
    //            string? content = component.TryGetProperty("content", out JsonElement contentElement) ? contentElement.GetString() : string.Empty;
    //            string? type = component.TryGetProperty("type", out JsonElement typeElement) ? typeElement.GetString() : string.Empty;
    //            string? id = component.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : string.Empty;
    //            string? key = component.TryGetProperty("key", out JsonElement keyElement) ? keyElement.GetString() : string.Empty;
    //            string? inputType = component.TryGetProperty("inputType", out JsonElement inputTypeElement) ? inputTypeElement.GetString() : string.Empty;

    //            if (type == "htmlelement")
    //            {
    //                viewModel.DynamicContents.Add(CreateLabel(content, id, FontSizeOption.Large));
    //            }

    //            if (type == "datagrid")
    //            {
    //                viewModel.DynamicContents.Add(CreateLabel(description, id, FontSizeOption.Medium));
    //            }

    //            if (type == "textfield")
    //            {
    //                viewModel.DynamicContents.Add(CreateTextField(label, key));
    //            }

    //            if (type == "textarea")
    //            {
    //                viewModel.DynamicContents.Add(CreateEditorField(label, key));
    //            }

    //            if (component.TryGetProperty("components", out JsonElement subComponents) && subComponents.ValueKind == JsonValueKind.Array)
    //            {
    //                processComponents(subComponents);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    //public class CompanyData
    //{
    //    public string? CompanyName { get; set; }
    //    public string? CompanyAge { get; set; }
    //    public string? CompanyType { get; set; }
    //    public DateTime? CompanyEstablished { get; set; }
    //    public string? RadioSelection { get; set; }
    //}

    //public CompanyData CollectData()
    //{
    //    var data = new CompanyData();

    //    foreach (var child in viewModel.DynamicContents.Children)
    //    {
    //        if (child is TextField textField)
    //        {
    //            if (textField.Title == "Company Name")
    //                data.CompanyName = textField.Text;
    //            else if (textField.Title == "Company Age")
    //                data.CompanyAge = textField.Text;
    //        }
    //        else if (child is PickerField pickerField)
    //        {
    //            data.CompanyType = pickerField.SelectedItem?.ToString();
    //        }
    //        else if (child is DatePickerField datePickerField)
    //        {
    //            data.CompanyEstablished = datePickerField.Date;
    //        }
    //        else if (child is RadioButtonGroupView radioGroup)
    //        {
    //            data.RadioSelection = radioGroup.SelectedItem?.ToString();
    //        }
    //    }

    //    return data;
    //}
}
