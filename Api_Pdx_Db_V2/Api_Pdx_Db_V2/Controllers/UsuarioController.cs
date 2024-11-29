﻿using Api_Pdx_Db_V2.Data;
using Api_Pdx_Db_V2.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace Api_Pdx_Db_V2.Controllers
{
    [ApiController]
    [Route("Api_Pdx_DbV2/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;
        public UsuarioController(DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }

       // este es de prueba apara ver si trae los datos no utilizar en produccion dado

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioModel>> GetUsuario()
        {
            return Ok(_conexionContext.usuario.ToList());
        }

        // Crear un nuevo usuario
        [HttpPost("Crear Usuario")]
        public ActionResult<UsuarioModel> CrearUsuario([FromBody] UsuarioModel nuevoUsuario)
        {
            nuevoUsuario.Pass = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.Pass);
            _conexionContext.usuario.Add(nuevoUsuario);
            _conexionContext.SaveChanges();

            var rol = _conexionContext.rol.FirstOrDefault(r => r.Descripcion == "User");

            if (rol != null)
            {
                // Crear la relación en la tabla Usuario_Rol
                var usuarioRol = new UsuarioRolModel
                {
                    IdUsuario = nuevoUsuario.Id, // Asignar el ID del nuevo usuario
                    IdRol = rol.Id // Asignar el ID del rol 'User'
                };

                // Agregar la relación a la tabla Usuario_Rol
                _conexionContext.usuario_rol.Add(usuarioRol);
                _conexionContext.SaveChanges();

                return Ok("Usuario y rol asignado exitosamente.");
            }
            else
            {
                return BadRequest("Rol 'User' no encontrado.");
            }

          
        } 

        // Obtener un usuario por ID
        [HttpGet("Usuario por id/{id}")]
        public ActionResult<UsuarioModel> GetUsuarioPorId(int id)
        {
            var usuario = _conexionContext.usuario.Find(id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(usuario);
        }

        // Eliminar un usuario
        [HttpDelete("Eliminar por id/ {id}")]
        public ActionResult EliminarUsuario(int id)
        {
            var usuario = _conexionContext.usuario.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            
            _conexionContext.usuario.Remove(usuario);

            _conexionContext.SaveChanges();

            return Ok("Usuario eliminado exitosamente.");
        }

        // Login de Usuario
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel login)
        {
            // Buscar usuario por UserName
            var usuario = _conexionContext.usuario.FirstOrDefault(u => u.UserName == login.UserName);

            if (usuario == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            // Verificar contraseña (hashed)
            if (!BCrypt.Net.BCrypt.Verify(login.Password, usuario.Pass))
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            // Aquí podrías generar un JWT o retornar un mensaje de éxito
            return Ok(new { mensaje = "Login exitoso" });
        }

    }
}
