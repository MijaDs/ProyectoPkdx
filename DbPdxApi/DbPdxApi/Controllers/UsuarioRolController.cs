using DbPdxApi.Data;
using DbPdxApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace DbPdxApi.Controllers
{
    [ApiController]
    [Route("DbPdxApi/[controller]")]
    public class UsuarioRolController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;

        public UsuarioRolController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }


        [HttpGet]
        public ActionResult<IEnumerable<UsuarioRolModel>> ObtenerRoles()
        {
            return Ok(_conexionContext.usuario_rol.ToList());
        }

        sdf
    }
}
