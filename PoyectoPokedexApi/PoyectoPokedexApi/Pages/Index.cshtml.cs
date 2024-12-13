using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoyectoPokedexApi.Utilities;

namespace PoyectoPokedexApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UsuarioApiClient _usuarioApiClient;
        private readonly ILogger<IndexModel> _logger;

        // Propiedad para almacenar el resultado de la conexi�n
        public string ConexionResultado { get; set; }

        public IndexModel(ILogger<IndexModel> logger, UsuarioApiClient usuarioApiClient)
        {
            _logger = logger;
            _usuarioApiClient = usuarioApiClient;
        }

        // M�todo manejador para validar la conexi�n
        public async Task<IActionResult> OnGetConexion()
        {
            try
            {
                await UsuarioApiClient.EjecutarTask(); // Llama al m�todo que realiza la solicitud
                return new JsonResult("Conexion Exitosa");
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }
    }
}
