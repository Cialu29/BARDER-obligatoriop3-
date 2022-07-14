using InternalServices.Models.Login;
using InternalServices.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Seguridad;

namespace InternalServices.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("api/Login/Autenticar")]
        public IHttpActionResult Autenticar([FromBody] LoginModel login)
        {
            try
            {
                SeguridadService seguridadService = new SeguridadService();

                var usuarioValidado = seguridadService.ValidarCredencialesUsuario(login.Username, login.Password);

                if (usuarioValidado != null)
                {
                    //Generacion de token JWT
                    var token = TokenGenerator.GenerateToken(login.Username);
                    usuarioValidado.Token = token;

                    return Ok(usuarioValidado);

                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
