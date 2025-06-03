
using OfficeAnywhere.Mobile.Models;
using OfficeAnywhere.Mobile.ViewModels;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace OfficeAnywhere.Mobile.Views;

public partial class TaskDetailPage : ContentPage
{
    public TaskDetailPage(TaskDetailViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}
