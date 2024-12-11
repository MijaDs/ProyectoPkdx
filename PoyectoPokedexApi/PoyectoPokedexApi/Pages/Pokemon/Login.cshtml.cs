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

        // Utiliza LogModel para recibir las credenciales de inicio de sesión
        [BindProperty]
        public LogModel Login { get; set; }

        public LoginModel(UsuarioApiClient usuarioApiClient)
        {
            _usuarioApiClient = usuarioApiClient;
            Login = new LogModel();
        }

        public void OnGet()
        {
            // Carga inicial de la página
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica si el modelo es válido antes de intentar el login.
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, retornamos la página con los errores de validación.
                return Page();
            }

            try
            {
                // Intentamos autenticar al usuario usando los datos proporcionados (Login es un objeto con las credenciales).
                var logResponse = await _usuarioApiClient.LoginUsuario(Login);

                // Comprobamos si el inicio de sesión fue exitoso
                if (logResponse != null && logResponse.Mensaje == "Login exitoso")
                {
                    // Si el login es exitoso, mostramos un mensaje y redirigimos a otra página.
                    UsuarioAutenticado = logResponse.Usuario;
                    MensajeLogin = "Inicio de sesión exitoso.";

                    // Redirige a la página de inicio de Pokémon en caso de éxito.
                    return RedirectToPage("/Pokemon/Index");
                }
                else
                {
                    // Si las credenciales no son válidas, mostramos un mensaje de error.
                    MensajeLogin = "Credenciales inválidas.";
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error en el proceso de login, capturamos la excepción y mostramos un mensaje.
                MensajeLogin = $"Error al intentar iniciar sesión: {ex.Message}";
            }

            // Si algo falla, regresamos a la misma página para que el usuario vea el mensaje de error.
            return Page();
        }
    }
}

