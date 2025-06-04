using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class TaskViewModel : ObservableObject
{
    private readonly TaskService _taskService;
    private readonly DelmonTaskCacheService _delmonTaskCacheService;

    public ICommand AddTaskCommand { get; }

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? profilePicture;

    [ObservableProperty]
    private ObservableCollection<TaskCard> taskCardList = new();

    private TaskCard? selectedTask;

    public TaskCard? SelectedTask
    {
        get => selectedTask;
        set
        {
            SetProperty(ref selectedTask, value);
            if (value != null)
            {
                TaskSelectedCommand.Execute(value);
                SelectedTask = null;
            }
        }
    }
    public TaskViewModel(TaskService taskService, DelmonTaskCacheService delmonTaskCacheService)
    {
        _taskService = taskService;
        _delmonTaskCacheService = delmonTaskCacheService;
        AddTaskCommand = new Command(async () => await AddTask());
    }

    public async Task InitializeAsync()
    {
        await GetMainUserAsync();
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
    public async Task LoadTaskDataAsync(CancellationToken cancellationToken)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var taskData = await _taskService.FetchTaskData(cancellationToken);

            if (taskData?.DelmonTasks != null)
            {
                var taskCards = await Task.Run(() =>
                    taskData.DelmonTasks.Select(task => new TaskCard
                    {
                        Title = task.Title,
                        TaskTypeName = task.TaskTypeName,
                        AddedDate = task.AddedDate,
                        LastUpdatedDate = task.LastUpdatedDate,
                        SenderUserImage = task.SenderUserImage,
                        EmployUserImage = task.EmployUserImage,
                        NotSameImage = task.SenderUserImage != task.EmployUserImage,
                        MessageCount = task.MessageCount,
                        DelmonTask = JsonSerializer.Serialize(task)
                    }).ToList(), cancellationToken);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TaskCardList.Clear();
                    foreach (var taskCard in taskCards)
                    {
                        TaskCardList.Add(taskCard);
                    }
                });
            }
        }
        catch (OperationCanceledException)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Snackbar.Make(
                    "Task loading was cancelled.",
                    duration: TimeSpan.FromSeconds(3),
                    visualOptions: new SnackbarOptions
                    {
                        BackgroundColor = Colors.Orange,
                        TextColor = Colors.White,
                        ActionButtonTextColor = Colors.White,
                        CornerRadius = 8,
                        Font = Microsoft.Maui.Font.Default
                    }
                ).Show();
            });
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
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
            });
        }
        finally
        {
            IsBusy = false;
        }
    }


    [RelayCommand]
    public async Task TaskSelectedAsync(TaskCard? selectedTask)
    {
        if (IsBusy || selectedTask == null) return;

        IsBusy = true;

        _delmonTaskCacheService.CacheTask(selectedTask.DelmonTask);
        await Shell.Current.GoToAsync("//TaskDetailPage", true);

        IsBusy = false;
    }

    private async Task AddTask()
    {
        await Shell.Current.GoToAsync($"//FormTemplate", true);
    }
}