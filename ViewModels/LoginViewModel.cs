using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthService _authService;

    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private string? password;

    [ObservableProperty]
    private UserAuthModel? userAuth;

    [ObservableProperty]
    private bool isBusy; // New property for spinner

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy) return; 

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await Snackbar.Make(
                "Please enter a username and password.",
                duration: TimeSpan.FromSeconds(5),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = Colors.DarkRed,
                    TextColor = Colors.White,
                    ActionButtonTextColor = Colors.White,
                    CornerRadius = 8,
                    Font = Microsoft.Maui.Font.Default
                }
            ).Show();
            return;
        }

        try
        {
            IsBusy = true;

            UserAuthModel userAuth = await _authService.LoginAsync(username, password);

            if (!string.IsNullOrEmpty(userAuth.AccessToken))
            {
                try
                {
                    await SecureStorage.SetAsync("UserId", userAuth.Id.ToString());
                    await SecureStorage.SetAsync("Tenant", userAuth.Tenant);

                    await SecureStorage.SetAsync("AccessToken", userAuth.AccessToken);
                    await SecureStorage.SetAsync("UserName", userAuth.UserName);
                    await SecureStorage.SetAsync("Email", userAuth.Email);
                    await SecureStorage.SetAsync("Role", userAuth.Roles.Any() ? userAuth.Roles.First() : "Default");

                    await Snackbar.Make(
                        $"Successfully Logged In: Welcome {userAuth.UserName}",
                        duration: TimeSpan.FromSeconds(5),
                        visualOptions: new SnackbarOptions
                        {
                            BackgroundColor = Colors.Green,
                            TextColor = Colors.White,
                            ActionButtonTextColor = Colors.White,
                            CornerRadius = 8,
                            Font = Microsoft.Maui.Font.Default
                        }
                    ).Show();

                    await Shell.Current.GoToAsync("//TaskPage");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", $"Failed to store user data: {ex.Message}", "OK");
                }
            }
            else
            {
                await Snackbar.Make(
                    userAuth.Message ?? "Invalid username or password.",
                    duration: TimeSpan.FromSeconds(5),
                    visualOptions: new SnackbarOptions
                    {
                        BackgroundColor = Colors.DarkRed,
                        TextColor = Colors.White,
                        ActionButtonTextColor = Colors.White,
                        CornerRadius = 8,
                        Font = Microsoft.Maui.Font.Default
                    }
                ).Show();
            }
        }
        catch (HttpRequestException ex)
        {
            await Shell.Current.DisplayAlert("Network Error", "Unable to connect to the server. Please check your internet connection.", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false; 
        }
    }
}