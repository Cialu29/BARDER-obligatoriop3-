using InternalServices.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models.Foto
{
    public class FotoModel
    {
        [Key]
        public int idFoto { get; set; }

        public int idUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string URL { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}