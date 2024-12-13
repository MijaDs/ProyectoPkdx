using Api_Pdx_Db_V2.Data;
using Api_Pdx_Db_V2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Pdx_Db_V2.Controllers
{
    [ApiController]
    [Route("Api_Pdx_DbV2/[controller]")]
    public class RetosController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;
        public RetosController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RetoModel>> GetRetos()
        {
            return Ok(_conexionContext.reto.ToList());
        }

        [HttpPut("CrearReto/{idUser1}/{idUser2}")]
        public async Task<ActionResult> CrearReto(int idUser1, int idUser2, [FromBody] RetoModel reto)
        {
            Console.WriteLine($"Recibido: idUser1={idUser1}, idUser2={idUser2}");

            // Obtener los usuarios desde la base de datos
            var usuario1 = await _conexionContext.usuario.FindAsync(idUser1);
            var usuario2 = await _conexionContext.usuario.FindAsync(idUser2);

            // Verificar si los usuarios existen
            if (usuario1 == null || usuario2 == null)
            {
                return NotFound("Uno o ambos usuarios no existen.");
            }

            var pokUsuario1 = await _conexionContext.usuario_pocket
                .Where(p => p.IdUsuario == idUser1) 
                .ToListAsync();

            if (!pokUsuario1.Any())
            {
                return BadRequest("El usuario 1 no tiene Pokémon en estado válido para participar.");
            }

            var idsPokemonsUsuario1 = pokUsuario1.Select(p => new { p.pkm_Id1, p.pkm_Id2, p.pkm_Id3 }).FirstOrDefault();

            if (idsPokemonsUsuario1 != null)
            {
                var pokemonsUsuario1 = await _conexionContext.usuario_pkm
                    .Where(p => p.IdUsuario == idUser1 &&
                                (p.Id == idsPokemonsUsuario1.pkm_Id1 ||
                                 p.Id == idsPokemonsUsuario1.pkm_Id2 ||
                                 p.Id == idsPokemonsUsuario1.pkm_Id3) &&
                                p.estado == 3) // Estado "Debilitado" (estado = 3)
                    .ToListAsync();

                if (pokemonsUsuario1.Any())
                {
                    return BadRequest("El usuario 1 tiene Pokémon debilitados y no puede participar.");
                }
            }

            var pokUsuario2 = await _conexionContext.usuario_pocket
                .Where(p => p.IdUsuario == idUser1)
                .ToListAsync();

            if (!pokUsuario1.Any())
            {
                return BadRequest("El usuario 1 no tiene Pokémon en estado válido para participar.");
            }

            var idsPokemonsUsuario2 = pokUsuario1.Select(p => new { p.pkm_Id1, p.pkm_Id2, p.pkm_Id3 }).FirstOrDefault();

            if (idsPokemonsUsuario1 != null)
            {
                var pokemonsUsuario1 = await _conexionContext.usuario_pkm
                    .Where(p => p.IdUsuario == idUser1 &&
                                (p.Id == idsPokemonsUsuario1.pkm_Id1 ||
                                 p.Id == idsPokemonsUsuario1.pkm_Id2 ||
                                 p.Id == idsPokemonsUsuario1.pkm_Id3) &&
                                p.estado == 3) // Estado "Debilitado" (estado = 3)
                    .ToListAsync();

                if (pokemonsUsuario1.Any())
                {
                    return BadRequest("El usuario 1 tiene Pokémon debilitados y no puede participar.");
                }
            }


            // Seleccionar un mensaje aleatorio de la tabla mensajesPred
            var mensajeAleatorio = await _conexionContext.mensajes
                .OrderBy(m => Guid.NewGuid()) // Selección aleatoria
                .Select(m => m.Mensaje)
                .FirstOrDefaultAsync();

            if (mensajeAleatorio == null)
            {
                return StatusCode(500, "No se pudo seleccionar un mensaje aleatorio.");
            }

            // Seleccionar aleatoriamente un ganador
            var ganador = (new Random().Next(0, 2) == 0) ? idUser1 : idUser2;
            var perdedor = (ganador == idUser1) ? idUser2 : idUser1;

            // Obtener los pkm_id relacionados con el usuario perdedor desde la tabla usuario_pkt
            var pokUsuarioPerdedor = await _conexionContext.usuario_pocket
                .Where(p => p.IdUsuario == perdedor)
                .ToListAsync();

            // Filtrar los pkm_id del perdedor (los Pokémon que participan en el reto)
            var pkmIdsPerdedor = pokUsuarioPerdedor.Select(p => new { p.pkm_Id1, p.pkm_Id2, p.pkm_Id3 }).FirstOrDefault();

            // Verificar que existen Pokémon para actualizar
            if (pkmIdsPerdedor != null)
            {
                // Actualizar solo los Pokémon relacionados con esos pkm_id en la tabla usuario_pkm
                var pokemonsPerdedor = await _conexionContext.usuario_pkm
                    .Where(p => p.IdUsuario == perdedor &&
                                (p.Id == pkmIdsPerdedor.pkm_Id1 ||
                                 p.Id == pkmIdsPerdedor.pkm_Id2 ||
                                 p.Id == pkmIdsPerdedor.pkm_Id3))
                    .ToListAsync();

                foreach (var pkm in pokemonsPerdedor)
                {
                    pkm.estado = 3; // Estado 'Debilitado'
                }

                // Guardar los cambios en la base de datos
                await _conexionContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("No se encontraron Pokémon asociados al reto para el perdedor.");
            }

            // Crear el objeto RetoModel y agregarlo a la base de datos
            var nuevoReto = new RetoModel
            {
                IdUsr1 = idUser1,
                IdUsr2 = idUser2,
                IdGanador = ganador,
                Mensaje = mensajeAleatorio
            };

            _conexionContext.reto.Add(nuevoReto);
            await _conexionContext.SaveChangesAsync();
            // Retornar una respuesta exitosa con detalles del reto
            return Ok($"Reto creado exitosamente entre Usuario {idUser1} y Usuario {idUser2} con mensaje: {mensajeAleatorio}");
        }
    }
 }
