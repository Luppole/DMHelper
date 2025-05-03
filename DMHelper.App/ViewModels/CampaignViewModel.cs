using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DMHelper.App.Models;
using DMHelper.App.Services;

namespace DMHelper.App.ViewModels;

public partial class CampaignViewModel : ObservableObject
{
    private readonly CampaignService _campaignService;

    [ObservableProperty]
    private ObservableCollection<Campaign> _campaigns = new();

    [ObservableProperty]
    private Campaign? _selectedCampaign;

    [ObservableProperty]
    private string _newCampaignName = string.Empty;

    [ObservableProperty]
    private string _newCampaignDescription = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    public CampaignViewModel(CampaignService campaignService)
    {
        _campaignService = campaignService;
        LoadCampaignsAsync();
    }

    private async void LoadCampaignsAsync()
    {
        IsBusy = true;
        var campaigns = await _campaignService.GetAllCampaignsAsync();
        Campaigns.Clear();
        foreach (var campaign in campaigns)
        {
            Campaigns.Add(campaign);
        }
        IsBusy = false;
    }

    [RelayCommand]
    private async Task CreateCampaignAsync()
    {
        if (string.IsNullOrWhiteSpace(NewCampaignName))
            return;

        var campaign = new Campaign
        {
            Name = NewCampaignName,
            Description = NewCampaignDescription
        };

        await _campaignService.CreateCampaignAsync(campaign);
        Campaigns.Add(campaign);

        NewCampaignName = string.Empty;
        NewCampaignDescription = string.Empty;
    }

    [RelayCommand]
    private async Task DeleteCampaignAsync()
    {
        if (SelectedCampaign == null)
            return;

        await _campaignService.DeleteCampaignAsync(SelectedCampaign.Id);
        Campaigns.Remove(SelectedCampaign);
        SelectedCampaign = null;
    }

    [RelayCommand]
    private async Task SaveCampaignAsync()
    {
        if (SelectedCampaign == null)
            return;

        await _campaignService.UpdateCampaignAsync(SelectedCampaign);
    }

    partial void OnSelectedCampaignChanged(Campaign? value)
    {
        if (value != null)
        {
            NewCampaignName = value.Name;
            NewCampaignDescription = value.Description;
        }
    }
} 