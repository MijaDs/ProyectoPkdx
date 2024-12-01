using Api_Pdx_Db_V2.Data;
using Api_Pdx_Db_V2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Pdx_Db_V2.Controllers
{
    public class UsuarioPktController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;
        public UsuarioPktController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioPktModel>> GetUsuarios_pkt()
        {
            return Ok(_conexionContext.usuario_pocket.ToList());
        }

        [HttpGet("{_idUsuario}")]
        public ActionResult<IEnumerable<UsuarioPktModel>> ObtenerPktUsuario(int _idUsuario)
        {
            var pktUsuario = _conexionContext.usuario_pocket.Where(up => up.IdUsuario == _idUsuario).ToList();
            if (pktUsuario== null|| !pktUsuario.Any())
            {
                return NotFound("Datos no encontrados");
            }
            return Ok(pktUsuario);
        }
        [HttpPost("Asignar-pocket")]
        public ActionResult asignarPocketUsuario(int _idUsuario) 
        {
            var pkmUsuario = _conexionContext.usuario_pkm.Where(up => up.IdUsuario==_idUsuario&& up.estado==1).ToList();
            if (pkmUsuario.Count < 3) {
                return BadRequest("Se necesitan al menos 3 pokemnon para asignar");
            }

            var pkmSeleccionados = pkmUsuario.Take(3).ToList();

            foreach (var pkm in pkmSeleccionados)
            {
               pkm.estado = 2; // Cambiar a estado "Poket"
            }
            // Crear un nuevo pocket
            
            var nuevoPocket = new UsuarioPktModel
            {
                IdUsuario = _idUsuario,
                pkmId_1 = pkmSeleccionados[0].Id,
                pkmId_2 = pkmSeleccionados[1].Id,
                pkmId_3 = pkmSeleccionados[2].Id

            };
            _conexionContext.usuario_pocket.Add(nuevoPocket);
            _conexionContext.SaveChanges();
            return Ok("Poket agregado");
        }

        [HttpPut("remplazar-pokemon")]
        public ActionResult ReemplazarPokt(int idUsuario, int pkmIdRemplazar, int idPkmNuevo)
        {
            var pocket = _conexionContext.usuario_pocket.FirstOrDefault(up => up.IdUsuario == idUsuario);
            if (pocket == null)
            {
                return NotFound("Pocket no encontrado para el usuario.");
            }
            // Verificar si el Pokémon a reemplazar está en el pocket

            if (pocket.pkmId_1 == pkmIdRemplazar)
            {
                pocket.pkmId_1 = idPkmNuevo;
            }
            else if (pocket.pkmId_2 == pkmIdRemplazar)
            {
                pocket.pkmId_2 = idPkmNuevo;
            }
            else if (pocket.pkmId_3 == pkmIdRemplazar)
            {
                pocket.pkmId_3 = idPkmNuevo;
            }
            else
            {
                return BadRequest("El Pokémon a reemplazar no se encuentra en el pocket.");
            }

            var pkmReemplazado = _conexionContext.usuario_pkm.Find(pkmIdRemplazar);
            if(pkmReemplazado != null)
            {
                pkmReemplazado.estado = 1;
            }
            var pkmNuevo = _conexionContext.usuario_pkm.Find(idPkmNuevo);
            if (pkmNuevo != null)
            {
                pkmNuevo.estado = 2; // Cambiar a estado "Poket"
            }
            _conexionContext.SaveChanges();
            return Ok("Pokémon reemplazado con éxito.");
        }
    }
}
