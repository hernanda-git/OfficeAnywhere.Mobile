using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using UraniumUI.Material.Controls;

namespace OfficeAnywhere.Mobile.Views;
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
    protected override void OnAppearing()
    {
        base.OnAppearing();

        FormLayout.Children.Clear();
        foreach (var view in viewModel.DynamicContents)
        {
            FormLayout.Children.Add(view);
        }
    }
    //private void PickerField_SelectedValueChanged(object sender, object e)
    //{
        
    //}
}
