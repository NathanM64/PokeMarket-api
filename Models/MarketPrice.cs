namespace api.Models;

public class MarketPrice
{
    public int Id { get; set; }
    public string CardId { get; set; } // Clé étrangère pointant vers PokemonCard
    public decimal? LowPrice { get; set; }
    public decimal? MidPrice { get; set; }
    public decimal? HighPrice { get; set; }
    public decimal? MarketPriceValue { get; set; }
    public DateTime UpdatedAt { get; set; }
}