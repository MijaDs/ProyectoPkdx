using Api_Pdx_Db_V2.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Pdx_Db_V2.Data
{
    public class DbConexionContext : DbContext
    {
        public DbConexionContext(DbContextOptions<DbConexionContext> options) : base(options) { }

        public DbSet<RolModel> rol { get; set; }
        public DbSet<UsuarioModel> usuario { get; set; }

        public DbSet<UsuarioRolModel> usuario_rol { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolModel>().ToTable("rol");

            modelBuilder.Entity<UsuarioModel>().ToTable("usuario");

            modelBuilder.Entity<UsuarioRolModel>().ToTable("usuario_rol");
        }
    }
}
