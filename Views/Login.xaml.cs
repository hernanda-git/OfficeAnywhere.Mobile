using OfficeAnywhere.Mobile.ViewModels;

namespace OfficeAnywhere.Mobile.Views
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
