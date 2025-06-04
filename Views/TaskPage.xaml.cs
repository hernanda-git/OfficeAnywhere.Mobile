using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.ViewModels;
using System.Text.Json;

namespace OfficeAnywhere.Mobile.Views
{
    public partial class TaskPage : ContentPage
    {
        private readonly TaskViewModel viewModel;
        private CancellationTokenSource? _cts = new CancellationTokenSource();

        public TaskPage(TaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _cts = new CancellationTokenSource();
            await Task.Run(viewModel.GetMainUserAsync);
            await viewModel.LoadTaskDataAsync(_cts.Token);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
