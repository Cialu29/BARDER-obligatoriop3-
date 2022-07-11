using Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SeguridadController : Controller
    {
        public ActionResult Index()
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
            }

            return View(InicioSesionModelo);
        }
    }
}