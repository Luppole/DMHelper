using System;
using System.Collections.Generic;

namespace DMHelper.App.Models;

public class Campaign
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public List<Session> Sessions { get; set; } = new();
    public List<NPC> NPCs { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
    public List<Player> Players { get; set; } = new();
}

public class Session
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime SessionDate { get; set; }
    public int SessionNumber { get; set; }
    public List<Encounter> Encounters { get; set; } = new();
}

public class NPC
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Personality { get; set; } = string.Empty;
    public string Background { get; set; } = string.Empty;
    public Dictionary<string, string> Relationships { get; set; } = new();
}

public class Location
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string MapPath { get; set; } = string.Empty;
    public List<string> ConnectedLocations { get; set; } = new();
}

public class Player
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public int Level { get; set; }
    public Dictionary<string, int> Stats { get; set; } = new();
}

public class Encounter
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Monster> Monsters { get; set; } = new();
    public List<Loot> Loot { get; set; } = new();
    public int Difficulty { get; set; }
}

public class Monster
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int ChallengeRating { get; set; }
    public int HitPoints { get; set; }
    public Dictionary<string, int> Stats { get; set; } = new();
    public List<string> Abilities { get; set; } = new();
}

public class Loot
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Value { get; set; }
    public bool IsMagical { get; set; }
} 