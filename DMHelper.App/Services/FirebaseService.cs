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
    private readonly string _basePath;

    public FirebaseService(IConfiguration configuration)
    {
        var firebaseConfig = configuration.GetSection("Firebase");
        var authDomain = firebaseConfig["AuthDomain"];
        
        // Extract the project ID from the auth domain
        var projectId = authDomain?.Split('.')[0];
        
        _basePath = "dmhelper";
        _firebaseClient = new FirebaseClient(
            $"https://{projectId}.firebaseio.com/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(firebaseConfig["ApiKey"])
            }
        );
    }

    public async Task<List<T>> GetCollectionAsync<T>(string collectionPath) where T : class
    {
        try
        {
            var collection = await _firebaseClient
                .Child(_basePath)
                .Child(collectionPath)
                .OnceAsync<T>();

            var result = new List<T>();
            foreach (var item in collection)
            {
                var dataWithId = item.Object;
                // If your object has an Id property, you can set it here:
                // typeof(T).GetProperty("Id")?.SetValue(dataWithId, item.Key);
                result.Add(dataWithId);
            }
            return result;
        }
        catch (Firebase.Database.FirebaseException ex) when (ex.Message.Contains("404"))
        {
            // Collection doesn't exist yet, return empty list
            return new List<T>();
        }
    }

    public async Task<string> AddItemAsync<T>(string collectionPath, T item) where T : class
    {
        var result = await _firebaseClient
            .Child(_basePath)
            .Child(collectionPath)
            .PostAsync(item);

        return result.Key;
    }

    public async Task<List<Campaign>> GetAllCampaignsAsync()
    {
        try
        {
            var campaigns = await GetCollectionAsync<Campaign>("campaigns");
            return campaigns;
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
                .Child(_basePath)
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

            var resultKey = await AddItemAsync("campaigns", campaign);
            campaign.Id = int.Parse(resultKey);
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
                .Child(_basePath)
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
                .Child(_basePath)
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
            var resultKey = await AddItemAsync("sessions", session);
            session.Id = int.Parse(resultKey);
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
            var resultKey = await AddItemAsync("npcs", npc);
            npc.Id = int.Parse(resultKey);
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
            var resultKey = await AddItemAsync("locations", location);
            location.Id = int.Parse(resultKey);
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
            var resultKey = await AddItemAsync("players", player);
            player.Id = int.Parse(resultKey);
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