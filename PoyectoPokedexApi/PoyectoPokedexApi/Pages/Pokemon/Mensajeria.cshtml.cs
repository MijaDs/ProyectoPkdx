using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoyectoPokedexApi.Utilities;
using PoyectoPokedexApi.Models.PokemonModel;
using PoyectoPokedexApi.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class MensajeriaModel : PageModel
    {
        private readonly PokeClient _pokeClient;

        public MensajeriaModel(PokeClient pokeClient)
        {
            _pokeClient = pokeClient;
        }

        public List<MensajeModel> MensajesEnviados { get; set; } = new List<MensajeModel>();
        public List<MensajeModel> MensajesRecibidos { get; set; } = new List<MensajeModel>();

        public async Task OnGetAsync()
        {
            // Simulaci�n de mensajes enviados y recibidos para poblar la vista.
            // En un escenario real, estos datos podr�an venir de una base de datos o API.

            MensajesEnviados = new List<MensajeModel>
            {
                new MensajeModel { Destinatario = "ash.ketchum123@gmail.com", Mensaje = "�Listo para la batalla? Mi Pikachu est� al 100%", Estado = "Enviado" },
                new MensajeModel { Destinatario = "misty.waterqueen@hotmail.com", Mensaje = "�Alguien sabe cu�l es la mejor estrategia contra un Starmie?", Estado = "Enviado" },
                new MensajeModel { Destinatario = "misty.waterqueen@hotmail.com", Mensaje = "Prep�rense para los problemas... �Qui�n quiere ver Enviado", Estado = "Enviado" }
            };

            MensajesRecibidos = new List<MensajeModel>
            {
                new MensajeModel { Destinatario = "ash.ketchum123@gmail.com", Mensaje = "Nos vemos en la arena", Estado = "Recibido" },
                new MensajeModel { Destinatario = "misty.waterqueen@hotmail.com", Mensaje = "Esto no se queda as�...", Estado = "Recibido" },
                new MensajeModel { Destinatario = "misty.waterqueen@hotmail.com", Mensaje = "Tu Pikachu es m�s fuerte", Estado = "Recibido" }
            };

            // Ejemplo de c�mo podr�as usar _pokeClient para obtener datos adicionales si fueran necesarios.
            // var pokemons = await _pokeClient.GetPokemons(new List<string> { "1", "4", "7" });
        }

        public class MensajeModel
        {
            public string Destinatario { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
        }
    }
}
