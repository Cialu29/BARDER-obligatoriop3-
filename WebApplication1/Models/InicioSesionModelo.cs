using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class InicioSesionModelo
    {
        //Registra
        [Required]
        [Display(Name = "Usuario")]
        public string Username { get; set; }


    }
}