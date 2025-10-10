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
        // En AppDbContext.cs
        public DbSet<Admin> Admins { get; set; }
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
        public DbSet<ItemPedido> ItemPedidos { get; set; }

        // Tablas intermedias (muchos a muchos)
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public DbSet<ComercioCategoria> ComercioCategorias { get; set; }
        public DbSet<ComercioHorario> ComercioHorarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.idadmin);
                entity.Property(e => e.usuario).IsRequired().HasMaxLength(45);
                entity.Property(e => e.password).IsRequired().HasMaxLength(255);
            });

            // Configuración de Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.idcliente);
                entity.Property(e => e.nombreCompleto).IsRequired().HasMaxLength(45);
                entity.Property(e => e.dni).IsRequired().HasMaxLength(25);
                entity.Property(e => e.celular).IsRequired().HasMaxLength(25);
                entity.Property(e => e.ciudad).IsRequired().HasMaxLength(45);
                entity.Property(e => e.calle).IsRequired().HasMaxLength(45);
                entity.Property(e => e.email).IsRequired().HasMaxLength(45);
                entity.Property(e => e.password).IsRequired().HasMaxLength(255);
            });

            // Configuración de Vehiculo
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.idvehiculo);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(45);
                entity.Property(e => e.patente).HasMaxLength(45);
                entity.Property(e => e.modelo).IsRequired().HasMaxLength(45);
                entity.Property(e => e.marca).IsRequired().HasMaxLength(45);
                entity.Property(e => e.companiaSeguros).HasMaxLength(45);
            });

            // Configuración de Repartidor
            // Configuración de Repartidor (ACTUALIZADA)
            modelBuilder.Entity<Repartidor>(entity =>
            {
                entity.HasKey(e => e.idrepartidor);
                entity.Property(e => e.nombreCompleto).IsRequired().HasMaxLength(45);
                entity.Property(e => e.dni).IsRequired().HasMaxLength(25);
                entity.Property(e => e.celular).IsRequired().HasMaxLength(25);
                entity.Property(e => e.ciudad).IsRequired().HasMaxLength(45);
                entity.Property(e => e.calle).IsRequired().HasMaxLength(45);
                entity.Property(e => e.email).IsRequired().HasMaxLength(45);
                entity.Property(e => e.password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.puntuacion).HasColumnType("decimal(10,1)");
                entity.Property(e => e.cvu).IsRequired().HasMaxLength(25);

                // SOLO mantener la relación con Vehiculo
                entity.HasOne(e => e.Vehiculo)
                      .WithMany()
                      .HasForeignKey(e => e.vehiculoIdVehiculo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de TarifaRepartidorLibre
            modelBuilder.Entity<TarifaRepartidorLibre>(entity =>
            {
                entity.HasKey(e => e.idtarifa);
                entity.Property(e => e.tarifaBase).HasColumnType("decimal(10,2)");
                entity.Property(e => e.kmRecorridos).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Repartidor)
                      .WithMany()
                      .HasForeignKey(e => e.RepartidorIdRepartidor)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de EstadoPedido
            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.idestado);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(45);
            });

            // Configuración de MetodoPagoPedido
            modelBuilder.Entity<MetodoPagoPedido>(entity =>
            {
                entity.HasKey(e => e.idmetodo);
                entity.Property(e => e.metodo).IsRequired().HasMaxLength(45);
            });

            // Configuración de Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.idpedido);
                entity.Property(e => e.codigo).IsRequired().HasMaxLength(45);
                entity.Property(e => e.subtotalPedido).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.ClienteIdCliente)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Repartidor)
                      .WithMany()
                      .HasForeignKey(e => e.RepartidorIdRepartidor)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.EstadoPedido)
                      .WithMany()
                      .HasForeignKey(e => e.EstadoPedidoIdEstado)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MetodoPagoPedido)
                      .WithMany()
                      .HasForeignKey(e => e.MetodoPagoPedidoIdMetodo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Stock
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.idstock);
                entity.Property(e => e.medida).HasMaxLength(45);
            });

            // Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.idproducto);
                entity.Property(e => e.nombre).IsRequired().HasMaxLength(45);
                entity.Property(e => e.fotoPortada).IsRequired().HasMaxLength(45);
                entity.Property(e => e.descripcion).IsRequired().HasMaxLength(255);
                entity.Property(e => e.unidadMedida).IsRequired().HasMaxLength(45);
                entity.Property(e => e.precioUnitario).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Stock)
                      .WithMany()
                      .HasForeignKey(e => e.StockIdStock)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Horarios
            modelBuilder.Entity<Horarios>(entity =>
            {
                entity.HasKey(e => e.idhorarios);
                entity.Property(e => e.dias).HasConversion<string>();
            });

            // Configuración de Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.idcategoria);
                entity.Property(e => e.nombre).IsRequired().HasMaxLength(45);
            });

            // Configuración de Comercio
            modelBuilder.Entity<Comercio>(entity =>
            {
                entity.HasKey(e => e.idcomercio);
                entity.Property(e => e.email).IsRequired().HasMaxLength(45);
                entity.Property(e => e.password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.nombreComercio).IsRequired().HasMaxLength(45);
                entity.Property(e => e.fotoPortada).IsRequired().HasMaxLength(45);
                entity.Property(e => e.celular).IsRequired().HasMaxLength(25);
                entity.Property(e => e.ciudad).IsRequired().HasMaxLength(45);
                entity.Property(e => e.calle).IsRequired().HasMaxLength(45);
                entity.Property(e => e.latitud).HasColumnType("decimal(10,7)");
                entity.Property(e => e.longitud).HasColumnType("decimal(10,7)");
                entity.Property(e => e.encargado).IsRequired().HasMaxLength(45);
                entity.Property(e => e.cvu).IsRequired().HasMaxLength(45);
                entity.Property(e => e.alias).IsRequired().HasMaxLength(45);
            });

            // Configuración de ItemPedido (NUEVA TABLA IMPORTANTE)
            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.HasKey(e => e.iditemPedido);
                entity.Property(e => e.cantProducto).IsRequired();
                entity.Property(e => e.precioFinal).HasColumnType("decimal(10,2)");
                entity.Property(e => e.total).HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Producto)
                      .WithMany()
                      .HasForeignKey(e => e.ProductoIdProducto)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Pedido)
                      .WithMany(p => p.ItemsPedido)
                      .HasForeignKey(e => e.PedidoIdPedido)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Comercio)
                      .WithMany()
                      .HasForeignKey(e => e.ComercioIdComercio)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de tablas many-to-many

            // CategoriaProducto
            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => new { e.CategoriaIdCategoria, e.ProductoIdProducto });

                entity.HasOne(e => e.Categoria)
                      .WithMany()
                      .HasForeignKey(e => e.CategoriaIdCategoria)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Producto)
                      .WithMany()
                      .HasForeignKey(e => e.ProductoIdProducto)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ComercioCategoria
            modelBuilder.Entity<ComercioCategoria>(entity =>
            {
                entity.HasKey(e => new { e.ComercioIdComercio, e.CategoriaIdCategoria });

                entity.HasOne(e => e.Comercio)
                      .WithMany(c => c.ComercioCategorias)
                      .HasForeignKey(e => e.ComercioIdComercio)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Categoria)
                      .WithMany()
                      .HasForeignKey(e => e.CategoriaIdCategoria)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ComercioHorario
            modelBuilder.Entity<ComercioHorario>(entity =>
            {
                entity.HasKey(e => new { e.ComercioIdComercio, e.HorariosIdHorarios });

                entity.HasOne(e => e.Comercio)
                      .WithMany(c => c.ComercioHorarios)
                      .HasForeignKey(e => e.ComercioIdComercio)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Horarios)
                      .WithMany()
                      .HasForeignKey(e => e.HorariosIdHorarios)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurar delete behavior para evitar ciclos
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
