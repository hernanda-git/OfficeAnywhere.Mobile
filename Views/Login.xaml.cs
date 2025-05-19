using OfficeAnywhere.Mobile.ViewModels;

namespace OfficeAnywhere.Mobile.Views
{
    public partial class Login : ContentPage
    {
        public Login(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevents back navigation, which is fine for a login page
            // If you want to allow back navigation or exit the app, modify this
            return true;
        }
    }
}