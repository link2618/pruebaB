using banco.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace banco.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/usuario")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService repository)
        {
            this._usuarioService = repository;
        }

        [HttpPost]
        [Route("ValidarLogin")]
        public IHttpActionResult ValidarLogin(USUARIO usuario)
        {
            try
            {
                var res = this._usuarioService.ValidarLogin(usuario);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest("Verifique sus credenciales.");
            }
        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        public IHttpActionResult RegistrarUsuario(USUARIO usuario)
        {
            try
            {
                var res = this._usuarioService.RegistrarUsuario(usuario);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
