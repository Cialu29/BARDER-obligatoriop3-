using InternalServices.Models.Foto;
using InternalServices.Models.UsuarioBardo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternalServices.Models.Usuario
{
    public class UsuarioModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsuarioModel()
        {
            UsuarioBardoes = new HashSet<UsuarioBardoModel>();
            UsuarioBardoes1 = new HashSet<UsuarioBardoModel>();
        }

        [Key]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<string> Fotoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UsuarioBardoModel> UsuarioBardoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<UsuarioBardoModel> UsuarioBardoes1 { get; set; }
    }
}