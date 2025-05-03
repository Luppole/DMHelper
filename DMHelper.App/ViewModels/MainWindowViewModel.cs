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

    public MainWindowViewModel(CampaignViewModel campaignViewModel)
    {
        _campaignViewModel = campaignViewModel;
    }

    [RelayCommand]
    private void NavigateTo(string view)
    {
        CurrentView = view;
    }
} 