using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace OfficeAnywhere.Mobile.ViewModels;

public partial class ExperimentViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<View> dynamicContents = new();

    public ExperimentViewModel()
    {

    }
}