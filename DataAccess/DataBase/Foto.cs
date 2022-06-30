namespace DataAccess.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Foto")]
    public partial class Foto
    {
        [Key]
        public int idFoto { get; set; }

        public int idUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string URL { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
