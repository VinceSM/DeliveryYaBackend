using Microsoft.EntityFrameworkCore;
using System;
using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        // Tablas principales
        public DbSet<IUserType> IUserTypes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }
        public DbSet<TarifaRepartidorLibre> TarifasRepartidorLibre { get; set; }
        public DbSet<EstadoPedido> EstadoPedidos { get; set; }
        public DbSet<MetodoPagoPedido> MetodoPagoPedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Horarios> Horarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<ItemPedido> ItemsPedido { get; set; }

        // Tablas intermedias (muchos a muchos)
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }  // categoria_has_producto
        public DbSet<ComercioCategoria> ComercioCategorias { get; set; }   // comercio_has_categoria
        public DbSet<ComercioHorario> ComercioHorarios { get; set; }       // comercio_has_horarios

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Hacerlo mas completo con todas las clases 
        }
    }
}
