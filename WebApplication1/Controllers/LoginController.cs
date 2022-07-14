using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransferObjects.Seguridad;
using WebApplication1.Models;
using WebApplication1.Seguridad;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion(InicioSesionModelo InicioSesionModel)
        {
            if (ModelState.IsValid)
            {
                IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.httpClient);

                var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDPOINT"];

                Uri autenticarUsuarioUrl = new Uri(webApiUrl + "Login/Autenticar");

                var response = apiClient.Post(autenticarUsuarioUrl, InicioSesionModel);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("Error de autenticacion", "Usuario o contrasena invalidos (como mi primo jaja)");
                }
                else
                {
                    SeguridadService seguridadService = new SeguridadService();

                    var usuario = JsonConvert.DeserializeObject<SesionUsuario>(response.Content.ReadAsStringAsync().Result);

                    seguridadService.GuardarSesionUsuario(usuario);

                    if (InicioSesionModel.RememberMe)
                    {
                        FormsAuthentication.SetAuthCookie(InicioSesionModel.Username, InicioSesionModel.RememberMe);
                    }


                    return RedirectToAction("", "");
                }
            }

            return View(InicioSesionModel);
        }
    }
}