using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.ViewModels;
using System.Text.Json;
using UraniumUI.Material.Controls;

namespace OfficeAnywhere.Mobile.Views
{
    public partial class FormTemplate : ContentPage
    {
        private readonly FormTemplateViewModel viewModel;

        public FormTemplate(FormTemplateViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            FormLayout.Children.Clear();
            foreach (var view in viewModel.DynamicContents)
            {
                FormLayout.Children.Add(view);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        #region DynamicFormBindings
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

       
        #endregion
    }
}
