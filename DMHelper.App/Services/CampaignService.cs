using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DMHelper.App.Data;
using DMHelper.App.Models;

namespace DMHelper.App.Services;

public class CampaignService
{
    private readonly ApplicationDbContext _context;

    public CampaignService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Campaign>> GetAllCampaignsAsync()
    {
        return await _context.Campaigns
            .Include(c => c.Sessions)
            .Include(c => c.NPCs)
            .Include(c => c.Locations)
            .Include(c => c.Players)
            .ToListAsync();
    }

    public async Task<Campaign?> GetCampaignByIdAsync(int id)
    {
        return await _context.Campaigns
            .Include(c => c.Sessions)
            .Include(c => c.NPCs)
            .Include(c => c.Locations)
            .Include(c => c.Players)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Campaign> CreateCampaignAsync(Campaign campaign)
    {
        campaign.CreatedDate = DateTime.UtcNow;
        campaign.LastModifiedDate = DateTime.UtcNow;
        
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
        return campaign;
    }

    public async Task<Campaign> UpdateCampaignAsync(Campaign campaign)
    {
        campaign.LastModifiedDate = DateTime.UtcNow;
        
        _context.Campaigns.Update(campaign);
        await _context.SaveChangesAsync();
        return campaign;
    }

    public async Task DeleteCampaignAsync(int id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign != null)
        {
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Session> AddSessionToCampaignAsync(int campaignId, Session session)
    {
        var campaign = await _context.Campaigns.FindAsync(campaignId);
        if (campaign == null)
            throw new ArgumentException("Campaign not found", nameof(campaignId));

        session.CampaignId = campaignId;
        campaign.Sessions.Add(session);
        campaign.LastModifiedDate = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<NPC> AddNPCToCampaignAsync(int campaignId, NPC npc)
    {
        var campaign = await _context.Campaigns.FindAsync(campaignId);
        if (campaign == null)
            throw new ArgumentException("Campaign not found", nameof(campaignId));

        npc.CampaignId = campaignId;
        campaign.NPCs.Add(npc);
        campaign.LastModifiedDate = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return npc;
    }

    public async Task<Location> AddLocationToCampaignAsync(int campaignId, Location location)
    {
        var campaign = await _context.Campaigns.FindAsync(campaignId);
        if (campaign == null)
            throw new ArgumentException("Campaign not found", nameof(campaignId));

        location.CampaignId = campaignId;
        campaign.Locations.Add(location);
        campaign.LastModifiedDate = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task<Player> AddPlayerToCampaignAsync(int campaignId, Player player)
    {
        var campaign = await _context.Campaigns.FindAsync(campaignId);
        if (campaign == null)
            throw new ArgumentException("Campaign not found", nameof(campaignId));

        player.CampaignId = campaignId;
        campaign.Players.Add(player);
        campaign.LastModifiedDate = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return player;
    }
} 