using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class TaskViewModel : ObservableObject
{
    private readonly TaskService _taskService;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? profilePicture;

    public TaskViewModel(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<MainUser?> GetMainUserAsync()
    {
        if (IsBusy) return null;
        try
        {
            IsBusy = true;
            var mainUser = await _taskService.FetchMainUser();
            ProfilePicture = mainUser?.UserImage ?? string.Empty;
            return mainUser;
        }
        catch (Exception ex)
        {
            await Snackbar.Make(
                $"Error fetching user: {ex.Message}",
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
            return null;
        }
        finally
        {
            IsBusy = false;
        }
    }
}