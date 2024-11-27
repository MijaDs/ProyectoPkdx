using DbPdxApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DbPdxApi.Data
{
    public class DbConexionContext : DbContext
    {
        public DbConexionContext(DbContextOptions<DbConexionContext> options):base(options) { }

        public DbSet<RolModel> rol { get; set; }

        public DbSet<UsuarioRolModel> usuario_rol { get; set; }

        public DbSet<UsuarioRolModel> usuario { get; set; }

        public DbSet<UsuarioPocketModel> usuario_pocket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolModel>().ToTable("rol");

            modelBuilder.Entity<UsuarioRolModel>().ToTable("usuario_rol");

            modelBuilder.Entity<UsuarioModel>().ToTable("usuario");

            modelBuilder.Entity<UsuarioModel>().ToTable("usuario_pocket");

        }
    }
        //Referenciar las tablas que se necesitan para poder utilizarla en los controlado







}