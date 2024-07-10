using api.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Services;

public class PokemonApiService
{
    private readonly HttpClient _httpClient;
    
    public PokemonApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CardSet>> GetPokemonSetsAsync()
    {
        var response = await _httpClient.GetStringAsync("https://api.pokemontcg.io/v2/sets/");
        if (string.IsNullOrEmpty(response))
        {
            throw new Exception("Failed to fetch data from Pokemon API.");
        }

        var data = JsonSerializer.Deserialize<PokemonSetApiResponse>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        if (data == null || data.Data == null)
        {
            throw new Exception("Failed to deserialize data from Pokemon API.");
        }

        var cardSets = new List<CardSet>();

        System.Console.WriteLine(data.Data);
        foreach (var set in data.Data)
        {
            cardSets.Add(new CardSet
            {
                Id = set.Id,
                Name = set.Name,
                Series = set.Series,
                PrintedTotal = set.PrintedTotal,
                ReleaseDate = DateTime.ParseExact(set.ReleaseDate, "yyyy/MM/dd", null),
                SymbolUrl = set.Images.Symbol,
                LogoUrl = set.Images.Logo
            });
        }

        return cardSets;
    }
    
    // Get cards from the Pokemon API by set ID
    public async Task<List<Card>> GetPokemonCardsAsync(string setId)
    {
        var response = await _httpClient.GetStringAsync($"https://api.pokemontcg.io/v2/cards?q=set.id:{setId}");
        if (string.IsNullOrEmpty(response))
        {
            throw new Exception("Failed to fetch data from Pokemon API.");
        }
        
        var data = JsonSerializer.Deserialize<PokemonCardApiResponse>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        if (data == null || data.Data == null)
        {
            throw new Exception("Failed to deserialize data from Pokemon API.");
        }

        var cards = new List<Card>();
        
        foreach (var card in data.Data)
        {
            System.Console.Out.WriteLine(JsonSerializer.Serialize(card));
            cards.Add(new Card
            {
                Id = card.Id,
                Name = card.Name,
                Supertype = card.Supertype,
                Subtypes = card.Subtypes != null ? string.Join(",", card.Subtypes) : null,
                Hp = card.Hp,
                Types = card.Types != null ? string.Join(",", card.Types) : null,
                Abilities = card.Abilities != null ? JsonSerializer.Serialize(card.Abilities) : null,
                Attacks = card.Attacks != null ? JsonSerializer.Serialize(card.Attacks) : null,
                Weaknesses = card.Weaknesses != null ? JsonSerializer.Serialize(card.Weaknesses) : null,
                RetreatCost = card.RetreatCost != null ? string.Join(",", card.RetreatCost) : null,
                ConvertedRetreatCost = card.ConvertedRetreatCost,
                SetId = card.Set.Id,
                Number = card.Number,
                Artist = card.Artist,
                Rarity = card.Rarity,
                SmallImageUrl = card.Images.Small,
                LargeImageUrl = card.Images.Large
            });
        }

        return cards;
    }

    
    private class PokemonSetApiResponse
    {
        public List<PokemonSet> Data { get; set; }
    }
    
    private class PokemonCardApiResponse
    {
        public List<PokemonCard> Data { get; set; }
    }
    
    private class PokemonSet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Series { get; set; }
        public int PrintedTotal { get; set; }
        public string ReleaseDate { get; set; }
        public Images Images { get; set; }
    }

    private class PokemonCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Supertype { get; set; }
        public List<string>? Subtypes { get; set; }
        public string Hp { get; set; }
        public List<string>? Types { get; set; }
        public List<CardAbilities>? Abilities { get; set; }
        public List<CardAttacks>? Attacks { get; set; }
        public List<CardWeakness>? Weaknesses { get; set; }
        public List<string>? RetreatCost { get; set; }
        public int ConvertedRetreatCost { get; set; }
        public PokemonSet Set { get; set; }
        public string Number { get; set; }
        public string Artist { get; set; }
        public string Rarity { get; set; }
        public CardImages Images { get; set; }
    }
    private class CardAbilities
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }
    
    private class CardAttacks
    {
        public string Name { get; set; }
        public List<string> Cost { get; set; }
        public int ConvertedEnergyCost { get; set; }
        public string Damage { get; set; }
        public string Text { get; set; }
    }
    
    private class CardWeakness
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
    
    private class CardImages
    {
        public string Small { get; set; }
        public string Large { get; set; }
    }
    
    
    private class Images
    {
        public string Symbol { get; set; }
        public string Logo { get; set; }
    }
}