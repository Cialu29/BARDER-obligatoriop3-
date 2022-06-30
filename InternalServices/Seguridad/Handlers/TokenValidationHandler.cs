using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace InternalServices.Seguridad.Handlers
{
    public class TokenValidationHandler
    {
        public static bool IntentarObtenerToken(HttpRequestMessage request, out string token)
        {
            token = null;
            bool tokenObtenido = false;
            IEnumerable<string> authHeaders;

            if (request.Headers.TryGetValues("Authorization", out authHeaders) && authHeaders.Count() == 1)
            {
                var bearerToken = authHeaders.ElementAt(0);

                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

                tokenObtenido = true;
            }

            return tokenObtenido;
        }

        public bool ValidarTiempoDeVidaToken(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null && DateTime.UtcNow < expires)
            {
                return true;
            }

            return false;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode codigoEstado;
            string token = string.Empty;

            //Obtener el token JWT del header de la solicitud
            if (!IntentarObtenerToken(request, out token))
            {
                codigoEstado = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                //Obtener configuraciones
                var claveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audiencia = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var emisor = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];

                //Validar token
                var claveSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(claveSecreta));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = audiencia,
                    ValidIssuer = emisor,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = this.ValidarTiempoDeVidaToken,
                    IssuerSigningKey = claveSeguridad
                };

                //Extraer y asignar el principal al hilo, idem para el usuario
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);

            }
            catch (SecurityTokenException)
            {
                codigoEstado = HttpStatusCode.Unauthorized;
            }
            catch(Exception)
            {
                codigoEstado = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(codigoEstado){ });
        }
    }
}