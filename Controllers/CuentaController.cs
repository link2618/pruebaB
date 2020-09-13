using banco.Models;
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
    [RoutePrefix("api/cuenta")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CuentaController : ApiController
    {
        private readonly ICuentaService _cuentaService;
        public CuentaController(ICuentaService repository)
        {
            this._cuentaService = repository;
        }

        [HttpPost]
        [Route("Transferencia")]
        public IHttpActionResult Transferencia(Transferencia transfer)
        {
            try
            {
                var res = this._cuentaService.Transferencia(transfer);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest("Ha ocurrido un error al realizar la transacción");
            }
        }
    }
}
