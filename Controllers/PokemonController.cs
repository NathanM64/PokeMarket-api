using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using api.Services;
using api.Models;
using System.Linq;
using System.Text.Json;
using Api.Database;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonApiService _pokemonApiService;
        private readonly ApiDbContext _context;

        public PokemonController(PokemonApiService pokemonApiService, ApiDbContext context)
        {
            _pokemonApiService = pokemonApiService;
            _context = context;
        }

        [HttpPost("update-sets")]
        public async Task<IActionResult> UpdatePokemonSets()
        {
            var cardSets = await _pokemonApiService.GetPokemonSetsAsync();

            foreach (var set in cardSets)
            {
                var existingSet = _context.CardSets.FirstOrDefault(s => s.Id == set.Id);
                if (existingSet == null)
                {
                    _context.CardSets.Add(set);
                }
                else
                {
                    existingSet.Name = set.Name;
                    existingSet.Series = set.Series;
                    existingSet.PrintedTotal = set.PrintedTotal;
                    existingSet.ReleaseDate = DateTime.ParseExact(set.ReleaseDate.ToString("yyyy/MM/dd"), "yyyy/MM/dd", null);
                    existingSet.SymbolUrl = set.SymbolUrl;
                    existingSet.LogoUrl = set.LogoUrl;
                    _context.CardSets.Update(existingSet);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Pokemon card sets updated successfully." });
        }
        
        [HttpPost("update-cards")]
        public async Task<IActionResult> UpdatePokemonCardsBySetId(string setId)
        {
            var cards = await _pokemonApiService.GetPokemonCardsAsync(setId);
            foreach (var card in cards)
            {
                var existingCard = _context.Cards.FirstOrDefault(c => c.Id == card.Id);
                if (existingCard == null)
                {
                    _context.Cards.Add(card);
                }
                else
                {
                    existingCard.Name = card.Name;
                    existingCard.SetId = card.SetId;
                    existingCard.Number = card.Number;
                    existingCard.Artist = card.Artist;
                    existingCard.Rarity = card.Rarity;
                    existingCard.SmallImageUrl = card.SmallImageUrl;
                    existingCard.LargeImageUrl = card.LargeImageUrl;
                    _context.Cards.Update(existingCard);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Pokemon cards updated successfully." });
        }
    }
}