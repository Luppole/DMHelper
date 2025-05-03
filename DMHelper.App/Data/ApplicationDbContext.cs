using Microsoft.EntityFrameworkCore;
using DMHelper.App.Models;
using System.Text.Json;

namespace DMHelper.App.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Campaign> Campaigns { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<NPC> NPCs { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Encounter> Encounters { get; set; } = null!;
    public DbSet<Monster> Monsters { get; set; } = null!;
    public DbSet<Loot> Loot { get; set; } = null!;

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

        // Configure Dictionary properties as JSON
        modelBuilder.Entity<NPC>()
            .Property(n => n.Relationships)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) ?? "{}",
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null) ?? new());

        modelBuilder.Entity<Player>()
            .Property(p => p.Stats)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) ?? "{}",
                v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions?)null) ?? new());

        modelBuilder.Entity<Monster>()
            .Property(m => m.Stats)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) ?? "{}",
                v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions?)null) ?? new());

        // Configure List properties as JSON
        modelBuilder.Entity<Location>()
            .Property(l => l.ConnectedLocations)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) ?? "[]",
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new());

        modelBuilder.Entity<Monster>()
            .Property(m => m.Abilities)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null) ?? "[]",
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new());
    }
} 