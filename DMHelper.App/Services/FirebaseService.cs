using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using DMHelper.App.Models;
using Newtonsoft.Json;

namespace DMHelper.App.Services;

public class FirebaseService
{
    private readonly FirebaseClient _firebaseClient;
    private const string BasePath = "dmhelper";

    public FirebaseService(IConfiguration configuration)
    {
        var firebaseConfig = configuration.GetSection("Firebase");
        var firebaseUrl = $"https://{firebaseConfig["ProjectId"]}.firebaseio.com";
        var authSecret = firebaseConfig["ApiKey"];

        _firebaseClient = new FirebaseClient(
            firebaseUrl,
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authSecret)
            });
    }

    public async Task<List<Campaign>> GetAllCampaignsAsync()
    {
        try
        {
            var campaigns = await _firebaseClient
                .Child(BasePath)
                .Child("campaigns")
                .OnceAsync<Campaign>();

            return campaigns.Select(x => x.Object).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to retrieve campaigns", ex);
        }
    }

    public async Task<Campaign?> GetCampaignByIdAsync(int id)
    {
        try
        {
            var campaign = await _firebaseClient
                .Child(BasePath)
                .Child("campaigns")
                .Child(id.ToString())
                .OnceSingleAsync<Campaign>();

            return campaign;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve campaign with ID {id}", ex);
        }
    }

    public async Task<Campaign> CreateCampaignAsync(Campaign campaign)
    {
        try
        {
            campaign.CreatedDate = DateTime.UtcNow;
            campaign.LastModifiedDate = DateTime.UtcNow;

            var result = await _firebaseClient
                .Child(BasePath)
                .Child("campaigns")
                .PostAsync(campaign);

            campaign.Id = int.Parse(result.Key);
            return campaign;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create campaign", ex);
        }
    }

    public async Task<Campaign> UpdateCampaignAsync(Campaign campaign)
    {
        try
        {
            campaign.LastModifiedDate = DateTime.UtcNow;

            await _firebaseClient
                .Child(BasePath)
                .Child("campaigns")
                .Child(campaign.Id.ToString())
                .PutAsync(campaign);

            return campaign;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update campaign with ID {campaign.Id}", ex);
        }
    }

    public async Task DeleteCampaignAsync(int id)
    {
        try
        {
            await _firebaseClient
                .Child(BasePath)
                .Child("campaigns")
                .Child(id.ToString())
                .DeleteAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete campaign with ID {id}", ex);
        }
    }

    public async Task<Session> AddSessionToCampaignAsync(int campaignId, Session session)
    {
        try
        {
            var campaign = await GetCampaignByIdAsync(campaignId);
            if (campaign == null)
                throw new Exception($"Campaign with ID {campaignId} not found");

            session.CampaignId = campaignId;
            var result = await _firebaseClient
                .Child(BasePath)
                .Child("sessions")
                .PostAsync(session);

            session.Id = int.Parse(result.Key);
            campaign.Sessions.Add(session);
            campaign.LastModifiedDate = DateTime.UtcNow;

            await UpdateCampaignAsync(campaign);
            return session;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add session to campaign {campaignId}", ex);
        }
    }

    public async Task<NPC> AddNPCToCampaignAsync(int campaignId, NPC npc)
    {
        try
        {
            var campaign = await GetCampaignByIdAsync(campaignId);
            if (campaign == null)
                throw new Exception($"Campaign with ID {campaignId} not found");

            npc.CampaignId = campaignId;
            var result = await _firebaseClient
                .Child(BasePath)
                .Child("npcs")
                .PostAsync(npc);

            npc.Id = int.Parse(result.Key);
            campaign.NPCs.Add(npc);
            campaign.LastModifiedDate = DateTime.UtcNow;

            await UpdateCampaignAsync(campaign);
            return npc;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add NPC to campaign {campaignId}", ex);
        }
    }

    public async Task<Location> AddLocationToCampaignAsync(int campaignId, Location location)
    {
        try
        {
            var campaign = await GetCampaignByIdAsync(campaignId);
            if (campaign == null)
                throw new Exception($"Campaign with ID {campaignId} not found");

            location.CampaignId = campaignId;
            var result = await _firebaseClient
                .Child(BasePath)
                .Child("locations")
                .PostAsync(location);

            location.Id = int.Parse(result.Key);
            campaign.Locations.Add(location);
            campaign.LastModifiedDate = DateTime.UtcNow;

            await UpdateCampaignAsync(campaign);
            return location;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add location to campaign {campaignId}", ex);
        }
    }

    public async Task<Player> AddPlayerToCampaignAsync(int campaignId, Player player)
    {
        try
        {
            var campaign = await GetCampaignByIdAsync(campaignId);
            if (campaign == null)
                throw new Exception($"Campaign with ID {campaignId} not found");

            player.CampaignId = campaignId;
            var result = await _firebaseClient
                .Child(BasePath)
                .Child("players")
                .PostAsync(player);

            player.Id = int.Parse(result.Key);
            campaign.Players.Add(player);
            campaign.LastModifiedDate = DateTime.UtcNow;

            await UpdateCampaignAsync(campaign);
            return player;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add player to campaign {campaignId}", ex);
        }
    }
} 