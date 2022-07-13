using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;
using WebApplication1.Seguridad;

namespace WebApplication1.Controllers
{
    public class SeguridadController : Controller
    {
        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion(InicioSesionModelo inicioSesionModelo)
        {
            if (ModelState.IsValid)
            {
                IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.httpClient);

                var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDOPOINT"];

                Uri autenticarUsuarioUrl = new Uri(webApiUrl + "Usuario/Autenticar");

                var response = apiClient.Post(autenticarUsuarioUrl, inicioSesionModelo);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("Error Autenticacion", "Usuario y/o contraseña inválidos");
                }
                else
                {
                    var informacionUsuario = JsonConvert.DeserializeObject<InicioSesionModelo>(response.Content.ReadAsStringAsync().Result);

                    SeguridadService.GuardarSesionUsuario(informacionUsuario);

                    if (InicioSesionModelo.RememberMe)
                    {
                        FormsAuthentication.SetAuthCookie(InicioSesionModelo.Username, InicioSesionModelo.RememberMe);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(InicioSesionModelo);
        }
    }
}