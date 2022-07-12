using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using TransferObjects.Seguridad;

namespace WebApplication1.Seguridad
{
    public class SeguridadService
    {
        public static void GuardarSesionUsuario(SesionUsuario usuario)
        {
            HttpContext.Current.Session["InformacionUsuario"] = usuario;
        }

        public static SesionUsuario ObtenerSesionUsuario()
        {
            return (SesionUsuario)HttpContent.Current.Sesion["InformacionUsuario"];
        }
    }
}