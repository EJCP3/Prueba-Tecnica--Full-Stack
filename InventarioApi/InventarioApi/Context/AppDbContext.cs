using Microsoft.EntityFrameworkCore;
using InventarioApi.Models;

namespace InventarioApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<Productos> Productos { get; set; }

        public DbSet<Users> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cliente 1 - N Ventas
            modelBuilder.Entity<Ventas>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteID);

            // Venta 1 - N Detalles
            modelBuilder.Entity<VentaDetalle>()
                .HasOne(d => d.Ventas)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.VentaID);

            // Producto 1 - N VentaDetalle
            modelBuilder.Entity<VentaDetalle>()
                .HasOne(d => d.Productos)
                .WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.ProductoID);
        }
    }
}
