namespace api.Models;

public class Card
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Supertype { get; set; }
    public string? Subtypes { get; set; } // Stocké en tant que chaîne de caractères séparée par des virgules
    public string? Hp { get; set; }
    public string? Types { get; set; } // Stocké en tant que chaîne de caractères séparée par des virgules
    public string? Abilities { get; set; } // Stocké en tant que chaîne JSON
    public string? Attacks { get; set; } // Stocké en tant que chaîne JSON
    public string? Weaknesses { get; set; } // Stocké en tant que chaîne JSON
    public string? RetreatCost { get; set; } // Stocké en tant que chaîne de caractères séparée par des virgules
    public int ConvertedRetreatCost { get; set; }
    public string SetId { get; set; } // Clé étrangère pointant vers CardSet
    public string Number { get; set; }
    public string Artist { get; set; }
    public string? Rarity { get; set; }
    public string SmallImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
}