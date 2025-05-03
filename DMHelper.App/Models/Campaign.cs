using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DMHelper.App.Models;

public class Campaign
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty("lastModifiedDate")]
    public DateTime LastModifiedDate { get; set; }

    [JsonProperty("sessions")]
    public List<Session> Sessions { get; set; } = new();

    [JsonProperty("npcs")]
    public List<NPC> NPCs { get; set; } = new();

    [JsonProperty("locations")]
    public List<Location> Locations { get; set; } = new();

    [JsonProperty("players")]
    public List<Player> Players { get; set; } = new();
}

public class Session
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("campaignId")]
    public int CampaignId { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonProperty("sessionDate")]
    public DateTime SessionDate { get; set; }

    [JsonProperty("sessionNumber")]
    public int SessionNumber { get; set; }

    [JsonProperty("encounters")]
    public List<Encounter> Encounters { get; set; } = new();
}

public class NPC
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("campaignId")]
    public int CampaignId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("race")]
    public string Race { get; set; } = string.Empty;

    [JsonProperty("class")]
    public string Class { get; set; } = string.Empty;

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("personality")]
    public string Personality { get; set; } = string.Empty;

    [JsonProperty("background")]
    public string Background { get; set; } = string.Empty;

    [JsonProperty("relationships")]
    public Dictionary<string, string> Relationships { get; set; } = new();
}

public class Location
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("campaignId")]
    public int CampaignId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("mapPath")]
    public string MapPath { get; set; } = string.Empty;

    [JsonProperty("connectedLocations")]
    public List<string> ConnectedLocations { get; set; } = new();
}

public class Player
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("campaignId")]
    public int CampaignId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("characterName")]
    public string CharacterName { get; set; } = string.Empty;

    [JsonProperty("race")]
    public string Race { get; set; } = string.Empty;

    [JsonProperty("class")]
    public string Class { get; set; } = string.Empty;

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("stats")]
    public Dictionary<string, int> Stats { get; set; } = new();
}

public class Encounter
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("sessionId")]
    public int SessionId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("monsters")]
    public List<Monster> Monsters { get; set; } = new();

    [JsonProperty("loot")]
    public List<Loot> Loot { get; set; } = new();

    [JsonProperty("difficulty")]
    public int Difficulty { get; set; }
}

public class Monster
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("challengeRating")]
    public int ChallengeRating { get; set; }

    [JsonProperty("hitPoints")]
    public int HitPoints { get; set; }

    [JsonProperty("stats")]
    public Dictionary<string, int> Stats { get; set; } = new();

    [JsonProperty("abilities")]
    public List<string> Abilities { get; set; } = new();
}

public class Loot
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("value")]
    public int Value { get; set; }

    [JsonProperty("isMagical")]
    public bool IsMagical { get; set; }
} 