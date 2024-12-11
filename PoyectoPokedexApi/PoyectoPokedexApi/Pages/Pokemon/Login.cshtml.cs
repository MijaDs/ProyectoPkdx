using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoyectoPokedexApi.Models.UsuarioModel;
using PoyectoPokedexApi.Utilities;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class LoginModel : PageModel
    {
        private readonly UsuarioApiClient _usuarioApiClient;

        // Propiedad para mostrar mensajes de estado en la vista
        public string MensajeLogin { get; private set; }

        public UsuarioModel UsuarioAutenticado { get; private set; }

        // Utiliza LogModel para recibir las credenciales de inicio de sesi�n
        [BindProperty]
        public LogModel Login { get; set; }

        public LoginModel(UsuarioApiClient usuarioApiClient)
        {
            _usuarioApiClient = usuarioApiClient;
            Login = new LogModel();
        }

        public void OnGet()
        {
            // Carga inicial de la p�gina
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica si el modelo es v�lido antes de intentar el login.
            if (!ModelState.IsValid)
            {
                // Si el modelo no es v�lido, retornamos la p�gina con los errores de validaci�n.
                return Page();
            }

            try
            {
                // Intentamos autenticar al usuario usando los datos proporcionados (Login es un objeto con las credenciales).
                var logResponse = await _usuarioApiClient.LoginUsuario(Login);

                // Comprobamos si el inicio de sesi�n fue exitoso
                if (logResponse != null && logResponse.Mensaje == "Login exitoso")
                {
                    // Si el login es exitoso, mostramos un mensaje y redirigimos a otra p�gina.
                    UsuarioAutenticado = logResponse.Usuario;
                    MensajeLogin = "Inicio de sesi�n exitoso.";

                    // Redirige a la p�gina de inicio de Pok�mon en caso de �xito.
                    return RedirectToPage("/Pokemon/Index");
                }
                else
                {
                    // Si las credenciales no son v�lidas, mostramos un mensaje de error.
                    MensajeLogin = "Credenciales inv�lidas.";
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error en el proceso de login, capturamos la excepci�n y mostramos un mensaje.
                MensajeLogin = $"Error al intentar iniciar sesi�n: {ex.Message}";
            }

            // Si algo falla, regresamos a la misma p�gina para que el usuario vea el mensaje de error.
            return Page();
        }
    }
}

