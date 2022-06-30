namespace DataAccess.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bardo")]
    public partial class Bardo
    {
        [Key]
        public int idBardo { get; set; }

        public bool Ganador { get; set; }

        [Required]
        [StringLength(25)]
        public string Estado { get; set; }

        public virtual UsuarioBardo UsuarioBardo { get; set; }
    }
}
