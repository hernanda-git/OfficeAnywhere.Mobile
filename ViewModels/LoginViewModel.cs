using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private string? password;

    public LoginViewModel()
    {
    }

    [RelayCommand]
    private static async Task LoginAsync()
    {
        bool isAuthenticated = true;

        if (isAuthenticated)
        {
            await Shell.Current.GoToAsync("//TaskPage");
        }
        else
        {
            await Snackbar.Make(
                "Login failed: Invalid credentials",
                duration: TimeSpan.FromSeconds(3),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = Colors.DarkRed,
                    TextColor = Colors.White,
                    CornerRadius = 8,
                    Font = Microsoft.Maui.Font.Default
                }
            ).Show();
        }
    }
}
