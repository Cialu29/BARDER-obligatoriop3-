using InternalServices.Models.Bardo;
using InternalServices.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InternalServices.Models.UsuarioBardo
{
    public class UsuarioBardoModel
    {
        public int idU1 { get; set; }

        public int idU2 { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idBardo { get; set; }

        public BardoModel Bardo { get; set; }

        public UsuarioModel Usuario { get; set; }

        public UsuarioModel Usuario1 { get; set; }
    }
}