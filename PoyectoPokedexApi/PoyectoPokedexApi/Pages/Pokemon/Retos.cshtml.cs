using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PoyectoPokedexApi.Models.PokemonModel;
using PoyectoPokedexApi.Models.UsuarioModel;
using PoyectoPokedexApi.Utilities;
using System.Net.Http;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class RetosModel : PageModel
    {
        private readonly PokeClient _pokeClient;
        private readonly UsuarioApiClient _usuarioApiClient;

        public List<RetoModel> ListaDeRetos { get; set; } = new List<RetoModel>();
        public UsuarioModel Usuario { get; set; }
        public List<PokemonModel> Pokemon { get; set; } = new List<PokemonModel>();

        public RetosModel(UsuarioApiClient usuarioApiClient, PokeClient pokeClient)
        {
            _usuarioApiClient = usuarioApiClient;
            _pokeClient = pokeClient;
        }

        public async Task OnGet()
        {
            // Verifica si el usuario está en TempData
            
        }

    }
}









