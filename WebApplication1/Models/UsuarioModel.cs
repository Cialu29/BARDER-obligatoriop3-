using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Resumen { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Contrasenia { get; set; }
        public ICollection<FotoModel> Fotoes { get; set; }

    }
}