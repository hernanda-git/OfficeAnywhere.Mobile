
using OfficeAnywhere.Mobile.ViewModels;

namespace OfficeAnywhere.Mobile.Views;

public partial class TaskDetailPage : ContentPage
{
    private readonly TaskDetailViewModel viewModel;

    public TaskDetailPage(TaskDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;

        viewModel.ViewsReady += () =>
        {
            FormLayout.Children.Clear();
            foreach (var view in viewModel.DynamicContents)
            {
                FormLayout.Children.Add(view);
            }
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.InitializeAsync();
    }
}
