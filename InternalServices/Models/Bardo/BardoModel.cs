using InternalServices.Models.UsuarioBardo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models.Bardo
{
    public class BardoModel
    {
        [Key]
        public int idBardo { get; set; }

        public bool Ganador { get; set; }

        [Required]
        [StringLength(25)]
        public string Estado { get; set; }

        public UsuarioBardoModel UsuarioBardo { get; set; }
    }
}