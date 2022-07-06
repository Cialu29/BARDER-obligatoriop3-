

namespace DataAccess.DataBase
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("UsuarioBardo")]
    public partial class UsuarioBardo
    {
        public int idU1 { get; set; }

        public int idU2 { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idBardo { get; set; }

        public virtual Bardo Bardo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }
    }
}
