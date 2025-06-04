using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.ViewModels;
using System.Text.Json;

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
