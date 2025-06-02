using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;
using OfficeAnywhere.Mobile.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class TaskViewModel : ObservableObject
{
    private readonly TaskService _taskService;

    public ICommand AddTaskCommand { get; }

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? profilePicture;

    [ObservableProperty]
    private ObservableCollection<TaskCard> taskCardList = new();

    public TaskViewModel(TaskService taskService)
    {
        _taskService = taskService;
        AddTaskCommand = new Command(async () => await AddTask());
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
            if (taskData?.DelmonTasks != null)
            {
                // Map DelmonTask to TaskCard
                var taskCards = taskData.DelmonTasks.Select(task => new TaskCard
                {
                    Title = task.Title,
                    TaskTypeName = task.TaskTypeName,
                    AddedDate = task.AddedDate,
                    LastUpdatedDate = task.LastUpdatedDate,
                    SenderUserImage = task.SenderUserImage,
                    EmployUserImage = task.EmployUserImage,
                    NotSameImage = task.SenderUserImage != task.EmployUserImage,
                    MessageCount = task.MessageCount
                }).ToList();

                // Update ObservableCollection
                TaskCardList.Clear();
                foreach (var taskCard in taskCards)
                {
                    TaskCardList.Add(taskCard);
                }
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

    [RelayCommand]
    public async Task TaskSelectedAsync(TaskCard selectedTask)
    {
        if (selectedTask != null)
        {
            await Shell.Current.GoToAsync("TaskDetailPage", new Dictionary<string, object>
                {
                    { "TaskCard", selectedTask }
                });
        }
    }

    private async Task AddTask()
    {
        await Shell.Current.GoToAsync($"//FormTemplate", true);
    }
}