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
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace WebApplication1.Controllers
{
    //[Authorized]
    public class UsuarioController : ApiController
    {
        // GET: Usuario
        public ActionResult Consulta(int id)
        {
            List<UsuarioModel> modelo = new List<UsuarioModel>();

            //Creamos una instancia del apiclient recibiendo una instancia unica para todo el sistema del HttpClient a traves del HttpClient
            IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.httpClient);

            //Creamos parametros para la query de la url
            var parameters = new Dictionary<string, string>()
            {
                ["Id"] = id.ToString()
            };

            //Obtenemos url base desde el web.config de la aplicacion web 
            var webApiUrl = ConfigurationManager.AppSettings["WEB:API_ENDPOINT"];

            //Formamos url para el metodo de consulta de clientes
            Uri consultaUsuariosUrl = new Uri(webApiUrl + "Uusario/GetUsuarioById");

            //Ejecutamos llamada GET
            var response = apiClient.Get(consultaUsuariosUrl, parameters);

            if (response.IsSuccessStatusCode)
            {
                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(response.Content.ReadAsStringAsync().Result);

                modelo.Add(usuario);
            }

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Creacion(UsuarioModel usuarioModel)
        {
            //Instancia apiClient para realizar llamadas HTTP a apis RESTful
            IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.httpClient);

            //Obtener url base desde web.config para la web api
            var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDOPOINT"];

            //Crear url para metodo de creacion de clientes
            Uri crearUsuarioUri = new Uri(webApiUrl + "Usuario/AddUsuario");

            //Ejecutar llamada http post y obtener respuesta
            var respose = apiClient.Post(crearUsuarioUri, usuarioModel);

            if (!respose.IsSuccessStatusCode)
            {
                ModelState.AddModelError("Error de API", "Ocurrio un error al intentar crear el usuario en el servidor remoto");
            }
            else
            {
                var usuarioCreado = JsonConvert.DeserializeObject<UsuarioModel>(respose.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Consulta", new { Id = usuarioCreado.idUsuario });
            }

            return View(usuarioModel);
        }
    }
}