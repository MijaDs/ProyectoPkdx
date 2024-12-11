using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoyectoPokedexApi.Models.PokemonModel;
using PoyectoPokedexApi.Utilities;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class IndexModel : PageModel
    {
        private readonly PokeClient _pokeClient;

        public PokemonModel pokemon { get; set; }
        public List<PokemonModel> pokemonList { get; set; }

        public IndexModel(PokeClient pokeClient)
        {
            _pokeClient = pokeClient;
        }

        public async Task OnGetAsync()
        {
            pokemon = await _pokeClient.GetPokemon("1");

            var pokemonIds = new List<string>();
            for (int i = 1; i <= 150; i++)  // Obt�n los primeros 150 Pok�mon
            {
                pokemonIds.Add(i.ToString());
            }

            pokemonList = await _pokeClient.GetPokemons(pokemonIds);  // Obtener los Pok�mon desde la API
        }

    }
}
