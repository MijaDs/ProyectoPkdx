using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PoyectoPokedexApi.Models.PokemonModel;
using PoyectoPokedexApi.Models.UsuarioModel;
using PoyectoPokedexApi.Utilities;
using System.Xml.Linq;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class UserModel : PageModel
    {
        private readonly PokeClient _pokeClient;

        private readonly UsuarioApiClient _usuarioApiClient;
        public UsuarioModel Usuario { get; set; }
        public List<UsuarioPkmModel> UsuarioPkms { get; set; } = new List<UsuarioPkmModel>();
        public List<PokemonModel> pokemon { get; set; } = new List<PokemonModel>();

        

        
        [BindProperty]
        public int PokemonId { get; set; }
        [BindProperty]
        public int PokemonIdReemplazar { get; set; }
        [BindProperty]
        
        public int NuevoPokemonId { get; set; }
        public PokemonModel pokeSearch { get; set; }

        public UserModel(UsuarioApiClient usuarioApiClient, PokeClient pokeClient)
        {
            _usuarioApiClient = usuarioApiClient;
            _pokeClient = pokeClient;
        }

        public async Task OnGet()
        {
            
            
            pokemon = new List<PokemonModel>();

            // Obtener el objeto Usuario de TempData
            if (TempData["Usuario"] != null)
            {
                Usuario = JsonConvert.DeserializeObject<UsuarioModel>(TempData["Usuario"].ToString());

                UsuarioPkms = (List<UsuarioPkmModel>)await _usuarioApiClient.ObtenerPkmsUsuario(Usuario.Id);

                // Iterar sobre los Pok�mon del usuario y obtener detalles
                foreach (var usuarioPkm in UsuarioPkms)
                {
                    var estado = await _usuarioApiClient.ObtenerEstado(usuarioPkm.estado);
                    var pokemonDetail = await _pokeClient.GetPokemon(usuarioPkm.pkm_id.ToString());

                    // Agregar a la lista con los detalles completos
                    pokemon.Add(new PokemonModel
                    {
                        id = usuarioPkm.pkm_id,
                        name = pokemonDetail.name,
                        sprites = pokemonDetail.sprites,
                        height = pokemonDetail.height,
                        weight = pokemonDetail.weight,
                        stats = pokemonDetail.stats,
                        types = pokemonDetail.types,
                        estado = estado.Estado
                    });


                }
               
                
                
                TempData["Usuario"] = JsonConvert.SerializeObject(Usuario);
            }
            else
            {
                
                Console.WriteLine("usuarioJson es null. No se puede deserializar el usuario.");
                
            }
        }


        public async Task<IActionResult> OnPostReemplazarPokemonAsync(int pokemonIdReemplazar, int nuevoPokemonId)
        {
            if (TempData["Usuario"] != null)
            {
                Usuario = JsonConvert.DeserializeObject<UsuarioModel>(TempData["Usuario"].ToString());

                
                var resultado = await _usuarioApiClient.ReemplazarPokemonAsync(Usuario.Id, pokemonIdReemplazar, nuevoPokemonId);

                // Vuelve a almacenar el objeto Usuario en TempData
                TempData["Usuario"] = JsonConvert.SerializeObject(Usuario);
            }

            
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAsignarPoketPokemonAsync(int pokemonId)
        {
            if (TempData["Usuario"] != null)
            {
                Usuario = JsonConvert.DeserializeObject<UsuarioModel>(TempData["Usuario"].ToString());

                // Llama al m�todo para asignar el Pok�mon al pocket del usuario
                var resultado = await _usuarioApiClient.AsignarPktAsync(Usuario.Id, pokemonId);

                // Vuelve a almacenar el objeto Usuario en TempData
                TempData["Usuario"] = JsonConvert.SerializeObject(Usuario);

                if (resultado)
                {
                   
                    return RedirectToPage(new { mensaje = "Pok�mon asignado al pocket con �xito." });
                }
                else
                {
                    
                    return RedirectToPage(new { mensaje = "Error al asignar el Pok�mon al pocket." });
                }
            }

            
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAgregarPokemonAsync(int pokemonId)
        {
            if (TempData["Usuario"] != null)
            {
                Usuario = JsonConvert.DeserializeObject<UsuarioModel>(TempData["Usuario"].ToString());

                // Llama al m�todo para agregar el Pok�mon a la Pokedex del usuario
                var resultado = await _usuarioApiClient.AgregarPokedexAsync(Usuario.Id, pokemonId);

                // Vuelve a almacenar el objeto Usuario en TempData
                TempData["Usuario"] = JsonConvert.SerializeObject(Usuario);

                if (resultado)
                {
                    
                    TempData["Mensaje"] = "Pok�mon agregado a la Pokedex con �xito.";
                }
                else
                {
                    
                    TempData["Mensaje"] = "Error al agregar el Pok�mon a la Pokedex.";
                }
            }

            
            return RedirectToPage();
        }
    }
}

