using Entidades;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Marcas> Marcas { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Compras> Compras { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source=..\RepuestoAntigua\Data\DBProyecto.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuarios>().HasData(
                new Usuarios { UsuarioId = 1, Clave = "admin",Fecha = DateTime.Now, Usuario="admin"}
                );
        }
    }

}
