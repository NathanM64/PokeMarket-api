using api.Models;
using System.Text.Json;

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

        var data = JsonSerializer.Deserialize<PokemonApiResponse>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        System.Console.WriteLine(data);
        if (data == null || data.Data == null)
        {
            throw new Exception("Failed to deserialize data from Pokemon API.");
        }

        var cardSets = new List<CardSet>();

        foreach (var set in data.Data)
        {
            System.Console.WriteLine(set);
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
    
    private class PokemonApiResponse
    {
        public List<PokemonSet> Data { get; set; }
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
    
    private class Images
    {
        public string Symbol { get; set; }
        public string Logo { get; set; }
    }
}