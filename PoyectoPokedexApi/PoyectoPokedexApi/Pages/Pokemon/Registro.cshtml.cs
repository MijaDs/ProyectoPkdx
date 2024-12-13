using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoyectoPokedexApi.Models.UsuarioModel;
using PoyectoPokedexApi.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace PoyectoPokedexApi.Pages.Pokemon
{
    public class RegistroModel : PageModel
    {
        private readonly UsuarioApiClient _usuarioApiClient;

        [BindProperty]
        public UsuarioModel Registro { get; set; }


        public string MensajeRegistro { get; private set; }
        public UsuarioModel Usuario { get; set; }

        public RegistroModel(UsuarioApiClient usuarioApiClient)
        {
            _usuarioApiClient = usuarioApiClient;
            //Usuario = new UsuarioModel();
        }

        public void OnGet()
        {
            // Esta función se ejecuta cuando se carga la página inicialmente
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            Usuario = new UsuarioModel
            {
                Id = 1,
                Nombre = "Juan Pérez",
                UserName = "juanperez",
                Pass = "contraseña123"
            };



            try
            {
                var logResponse = await _usuarioApiClient.Registro(Usuario);

                if (logResponse != null && logResponse.Mensaje == "creado")
                {
                    MensajeRegistro = "Registro exitoso.";

                    return RedirectToPage("/Pokemon/Index");
                }
                else
                {
                    MensajeRegistro = "Datos erroneos.";
                }
            }
            catch (Exception ex)
            {
                MensajeRegistro = $"Error al intentar registrarse: {ex.Message}";
            }

            return Page();
        }

        private string HashContraseña(string contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña a un array de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

                // Convertir el array de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
