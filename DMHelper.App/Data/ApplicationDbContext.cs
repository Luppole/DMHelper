using Microsoft.EntityFrameworkCore;
using DMHelper.App.Models;

namespace DMHelper.App.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<NPC> NPCs { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<Loot> Loot { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Campaign
        modelBuilder.Entity<Campaign>()
            .HasMany(c => c.Sessions)
            .WithOne()
            .HasForeignKey(s => s.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Campaign>()
            .HasMany(c => c.NPCs)
            .WithOne()
            .HasForeignKey(n => n.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Campaign>()
            .HasMany(c => c.Locations)
            .WithOne()
            .HasForeignKey(l => l.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Campaign>()
            .HasMany(c => c.Players)
            .WithOne()
            .HasForeignKey(p => p.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Session
        modelBuilder.Entity<Session>()
            .HasMany(s => s.Encounters)
            .WithOne()
            .HasForeignKey(e => e.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Encounter
        modelBuilder.Entity<Encounter>()
            .HasMany(e => e.Monsters)
            .WithOne()
            .HasForeignKey(m => m.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Encounter>()
            .HasMany(e => e.Loot)
            .WithOne()
            .HasForeignKey(l => l.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 