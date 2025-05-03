using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DMHelper.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _currentView = "Campaign";

    [RelayCommand]
    private void NavigateTo(string view)
    {
        CurrentView = view;
    }

    public MainWindowViewModel()
    {
        Title = "D&D Dungeon Master Assistant";
    }
} 