using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMHelper.App.Models;

namespace DMHelper.App.Services;

public class CampaignService
{
    private readonly FirebaseService _firebaseService;
    private const string CollectionPath = "campaigns";

    public CampaignService(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<List<Campaign>> GetAllCampaignsAsync()
    {
        try
        {
            var campaigns = await _firebaseService.GetCollectionAsync<Campaign>(CollectionPath);
            return campaigns ?? new List<Campaign>();
        }
        catch (System.Exception ex)
        {
            // Log the error
            System.Diagnostics.Debug.WriteLine($"Error getting campaigns: {ex}");
            return new List<Campaign>();
        }
    }

    public async Task<string> CreateCampaignAsync(Campaign campaign)
    {
        return await _firebaseService.AddItemAsync(CollectionPath, campaign);
    }
}