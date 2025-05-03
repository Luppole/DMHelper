using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMHelper.App.Models;

namespace DMHelper.App.Services;

public class CampaignService
{
    private readonly FirebaseService _firebaseService;

    public CampaignService(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<List<Campaign>> GetAllCampaignsAsync()
    {
        return await _firebaseService.GetAllCampaignsAsync();
    }

    public async Task<Campaign?> GetCampaignByIdAsync(int id)
    {
        return await _firebaseService.GetCampaignByIdAsync(id);
    }

    public async Task<Campaign> CreateCampaignAsync(Campaign campaign)
    {
        return await _firebaseService.CreateCampaignAsync(campaign);
    }

    public async Task<Campaign> UpdateCampaignAsync(Campaign campaign)
    {
        return await _firebaseService.UpdateCampaignAsync(campaign);
    }

    public async Task DeleteCampaignAsync(int id)
    {
        await _firebaseService.DeleteCampaignAsync(id);
    }

    public async Task<Session> AddSessionToCampaignAsync(int campaignId, Session session)
    {
        return await _firebaseService.AddSessionToCampaignAsync(campaignId, session);
    }

    public async Task<NPC> AddNPCToCampaignAsync(int campaignId, NPC npc)
    {
        return await _firebaseService.AddNPCToCampaignAsync(campaignId, npc);
    }

    public async Task<Location> AddLocationToCampaignAsync(int campaignId, Location location)
    {
        return await _firebaseService.AddLocationToCampaignAsync(campaignId, location);
    }

    public async Task<Player> AddPlayerToCampaignAsync(int campaignId, Player player)
    {
        return await _firebaseService.AddPlayerToCampaignAsync(campaignId, player);
    }
} 