using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace InternalServices.Seguridad
{
    public class TokenGenerator
    {
        public static string GenerateToken(string email)
        {
            string token = string.Empty;

            //Obtener configuraciones para creacion de token JWT
            var claveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audiencia = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var emisor = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var tiempoDeVida = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            //Generar clave de seguridad
            var claveDeSeguridad = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(claveSecreta));

            //Crear credenciales para firma digital
            var credencialesDeFirma = new SigningCredentials(claveDeSeguridad, SecurityAlgorithms.HmacSha256Signature);

            //Crear claims de identidad
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) });

            //Crear token JWT
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audiencia,
                issuer: emisor,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(tiempoDeVida)),
                signingCredentials: credencialesDeFirma);

            token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}