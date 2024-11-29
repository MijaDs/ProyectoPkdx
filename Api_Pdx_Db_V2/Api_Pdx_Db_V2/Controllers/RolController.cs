﻿using Api_Pdx_Db_V2.Data;
using Api_Pdx_Db_V2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Pdx_Db_V2.Controllers
{
    [ApiController]
    [Route("Api_Pdx_DbV2/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly DbConexionContext _conexionContext;
        public RolController (DbConexionContext conexionContext)
        {
            _conexionContext = conexionContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RolModel>> GetRoles() 
        {
            return Ok(_conexionContext.rol.ToList());
        }
    }
}
