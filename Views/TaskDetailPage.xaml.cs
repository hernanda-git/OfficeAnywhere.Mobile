
using OfficeAnywhere.Mobile.Models;

namespace OfficeAnywhere.Mobile.Views;

[QueryProperty(nameof(TaskCard), "TaskCard")]
public partial class TaskDetailPage : ContentPage
{
    TaskCard? taskCard;
    public TaskCard? TaskCard
    {
        get => taskCard;
        set
        {
            taskCard = value;
            OnPropertyChanged();
        }
    }

    public TaskDetailPage() => InitializeComponent();
}

