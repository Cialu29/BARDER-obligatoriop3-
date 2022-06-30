using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.DataBase
{
    public partial class Barderbd : DbContext
    {
        public Barderbd()
            : base("name=Barderbd1")
        {
        }

        public virtual DbSet<Bardo> Bardoes { get; set; }
        public virtual DbSet<Foto> Fotoes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioBardo> UsuarioBardoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bardo>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Bardo>()
                .HasOptional(e => e.UsuarioBardo)
                .WithRequired(e => e.Bardo);

            modelBuilder.Entity<Foto>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Resumen)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Fotoes)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.UsuarioBardoes)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.idU1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.UsuarioBardoes1)
                .WithRequired(e => e.Usuario1)
                .HasForeignKey(e => e.idU2)
                .WillCascadeOnDelete(false);
        }
    }
}
