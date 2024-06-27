using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using api.Services;
using api.Models;
using System.Linq;
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
            return Ok(new { message = "Pokemon sets updated successfully." });
        }
    }
}