using DbPdxApi.Data;
using DbPdxApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbPdxApi.Controllers
{
    [ApiController]
    [Route("DbPdxApi/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;

        public UsuarioController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerRoles()
        {
            return Ok(_conexionContext.usuario.ToList());
        }
    }
}
