using DbPdxApi.Data;
using DbPdxApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbPdxApi.Controllers
{
        [ApiController]
        [Route("DbPdxApi/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;

        public RolController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }

        [HttpGet]
        public ActionResult <IEnumerable<RolModel>> ObtenerRoles()
        {
            return Ok(_conexionContext.rol.ToList());
        }
    }
}
