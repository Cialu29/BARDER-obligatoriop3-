using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Authorized]
    public class UsuarioController : ApiController
    {
        // GET: Usuario
        public ActionResult Consulta(int id)
        {
            List<UsuarioModel> modelo = new List<UsuarioModel>();

            IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.httpClient);

            var parameters = new Dictionary<string, string>()
            {
                ["Id"] = Id.ToString()
            };

            var webApiUrl = ConfigurationManager.AppSettings["WEB:API_ENDPOINT"];

            Uri consultaUsuariosUrl = new Uri(webApiUrl + "Uusario/GetUsuarioById");

            var response = apiClient.Get(consultaUsuariosUrl, parameters);

            if (response.IsSuccessStatusCode)
            {
                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Content.ReadAsStringAsync().Result);

                modelo.Add(usuario);
            }

            return View(modelo);
        }
    }
}