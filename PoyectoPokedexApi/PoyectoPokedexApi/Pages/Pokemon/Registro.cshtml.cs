using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoyectoPokedexApi.Models;
using PoyectoPokedexApi.Models.UsuarioModel;
using PoyectoPokedexApi.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class RegistroModel : PageModel
    {
        private readonly UsuarioApiClient _usuarioApiClient;




            [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Nombre { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public RegistroModel(UsuarioApiClient usuarioApiClient)
        {
            _usuarioApiClient = usuarioApiClient;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var usuario = new UsuarioModel
                {
                    Id = Id,
                    Nombre = Nombre,
                    UserName = UserName,
                    Password = Password
                };

                var result = await _usuarioApiClient.Registro(usuario);

                // Redirigir al usuario a una página de éxito o login después del registro
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                // Manejar errores, por ejemplo, mostrando un mensaje en la misma página
                ModelState.AddModelError(string.Empty, $"Error al registrarse: {ex.Message}");
                return Page();
            }
        }
    }
}