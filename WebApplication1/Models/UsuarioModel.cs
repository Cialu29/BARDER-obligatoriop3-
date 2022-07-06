using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }

        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(25)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(25)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Resumen { get; set; }

        [Required]
        [StringLength(25)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(25)]
        public string Pais { get; set; }

        [Required]
        [StringLength(8)]
        public string Contrasenia { get; set; }

        [Required]
        [StringLength(25)]
        public string Salt { get; set; }

        [Display("Foto de perfil")]
        public HttpPostedFileBase Foto1 { get; set; }

        public HttpPostedFileBase Foto2 { get; set; }

        public HttpPostedFileBase Foto3 { get; set; }

        public List<string> Fotos { get; set; }
    }
}