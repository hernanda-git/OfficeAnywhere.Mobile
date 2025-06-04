
using OfficeAnywhere.Mobile.ViewModels;

namespace OfficeAnywhere.Mobile.Views;

public partial class TaskDetailPage : ContentPage
{
    private readonly TaskDetailViewModel viewModel;

    public TaskDetailPage(TaskDetailViewModel viewModel)
    {
        BindingContext = this.viewModel = viewModel;
        InitializeComponent();
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
}
