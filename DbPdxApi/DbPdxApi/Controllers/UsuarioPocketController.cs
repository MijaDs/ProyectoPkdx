using DbPdxApi.Data;
using DbPdxApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbPdxApi.Controllers
{
    [ApiController]
    [Route("DbPdxApi/[controller]")]
    public class UsuarioPocketController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;

        public UsuarioPocketController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioPocketModel>> ObtenerRoles()
        {
            return Ok(_conexionContext.usuario_rol.ToList());
        }
         
    }
}     