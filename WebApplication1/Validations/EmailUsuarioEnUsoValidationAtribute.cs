using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Validations
{
    public class EmailUsuarioEnUsoValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                IWebApiClient apiClient = new WebApiClient(HttpClientAccessor.httpClient);

                var parameters = new Dictionary<string, string>()
                {
                    ["Email"] = value.ToString()
                };

                var webApiUrl = ConfigurationManager.AppSettings["WEB_API_ENDPOINT"];

                Uri consultaUsuariosUrl = new Uri(webApiUrl + "Usuario/ExistUsuarioByEmail");

                var response = apiClient.Get(consultaUsuariosUrl, parameters);

                if (response.IsSuccessStatusCode)
                {
                    var existeUsuario = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);

                    if (existeUsuario)
                    {
                        return new ValidationResult("El mail ya esta en uso");
                    }
                }
                else
                {
                    return new ValidationResult("No es posible validar usuario");
                }
            }

            return ValidationResult.Success;
        }
    }
}