namespace DataAccess.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Fotoes = new HashSet<Foto>();
            UsuarioBardoes = new HashSet<UsuarioBardo>();
            UsuarioBardoes1 = new HashSet<UsuarioBardo>();
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
        public virtual ICollection<Foto> Fotoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioBardo> UsuarioBardoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioBardo> UsuarioBardoes1 { get; set; }
    }
}
