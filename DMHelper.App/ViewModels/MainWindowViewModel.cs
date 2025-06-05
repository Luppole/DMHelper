using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DMHelper.App.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "D&D Dungeon Master Assistant Suite";

    [ObservableProperty]
    private string _currentView = "Campaign";

    [ObservableProperty]
    private CampaignViewModel _campaignViewModel;

    [ObservableProperty]
    private CombatTrackerViewModel _combatTrackerViewModel;

    public MainWindowViewModel(CampaignViewModel campaignViewModel, CombatTrackerViewModel combatTrackerViewModel)
    {
        _campaignViewModel = campaignViewModel;
        _combatTrackerViewModel = combatTrackerViewModel;
    }

    [RelayCommand]
    private void NavigateTo(string view)
    {
        CurrentView = view;
    }
} 