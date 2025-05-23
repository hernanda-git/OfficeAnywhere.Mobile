using OfficeAnywhere.Mobile.ViewModels;

namespace OfficeAnywhere.Mobile.Views
{
    public partial class TaskPage : ContentPage
    {
        private readonly TaskViewModel viewModel;

        public TaskPage(TaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadTaskDataAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
