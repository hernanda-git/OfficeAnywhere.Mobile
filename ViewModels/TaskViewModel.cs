using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;
using System.Collections.ObjectModel;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class TaskViewModel : ObservableObject
{
    private readonly TaskService _taskService;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? profilePicture;

    [ObservableProperty]
    private ObservableCollection<TaskData> taskDataList = new();

    public TaskViewModel(TaskService taskService)
    {
        _taskService = taskService;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await GetMainUserAsync();
        await LoadTaskDataAsync();
    }

    [RelayCommand]
    public async Task GetMainUserAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var mainUser = await _taskService.FetchMainUser();
            ProfilePicture = "https://www.o-anywhere.com/" + mainUser?.UserImage ?? string.Empty;
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
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task LoadTaskDataAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var taskData = await _taskService.FetchTaskData();
            if (taskData != null)
            {
                TaskDataList.Clear();
                TaskDataList.Add(taskData);
            }
        }
        catch (Exception ex)
        {
            await Snackbar.Make(
                $"Error fetching task data: {ex.Message}",
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
        finally
        {
            IsBusy = false;
        }
    }
}