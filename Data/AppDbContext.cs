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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }
        public DbSet<TarifaRepartidorLibre> TarifasRepartidorLibre { get; set; }
        public DbSet<EstadoPedido> EstadoPedidos { get; set; }
        public DbSet<MetodoPagoPedido> MetodoPagoPedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
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

            // --- Admin ---
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.idadmin);
                entity.Property(e => e.usuario).IsRequired().HasMaxLength(45);
                entity.Property(e => e.password).IsRequired().HasMaxLength(255);
            });

            // --- Categoria ---
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categoria");

                entity.HasKey(e => e.idcategoria);

                entity.Property(e => e.idcategoria)
                    .HasColumnName("idcategoria")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.createdAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.deletedAt)
                    .HasColumnName("deletedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.updatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });


            // --- Cliente ---
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.HasKey(e => e.idcliente);

                entity.Property(e => e.idcliente)
                    .HasColumnName("idcliente")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.nombreCompleto)
                    .HasColumnName("nombreCompleto")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.dni)
                    .HasColumnName("dni")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.nacimiento)
                    .HasColumnName("nacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.celular)
                    .HasColumnName("celular")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.calle)
                    .HasColumnName("calle")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.numero)
                    .HasColumnName("numero");

                entity.Property(e => e.email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.createdAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.updatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.deletedAt)
                    .HasColumnName("deletedAt")
                    .HasColumnType("datetime");
            });


            // --- Vehiculo ---
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.idvehiculo);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(45);
                entity.Property(e => e.patente).HasMaxLength(45);
                entity.Property(e => e.modelo).IsRequired().HasMaxLength(45);
                entity.Property(e => e.marca).IsRequired().HasMaxLength(45);
                entity.Property(e => e.companiaSeguros).HasMaxLength(45);
            });

            // --- Repartidor ---
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

                entity.HasOne(e => e.Vehiculo)
                      .WithMany()
                      .HasForeignKey(e => e.vehiculoIdVehiculo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // --- TarifaRepartidorLibre ---
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

            // --- EstadoPedido ---
            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.idestado);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(45);
            });

            // --- MetodoPagoPedido ---
            modelBuilder.Entity<MetodoPagoPedido>(entity =>
            {
                entity.HasKey(e => e.idmetodo);
                entity.Property(e => e.metodo).IsRequired().HasMaxLength(45);
            });

            // --- Pedido ---
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedido");

                // Clave primaria
                entity.HasKey(e => e.idpedido);

                // Propiedades
                entity.Property(e => e.fecha)
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.hora)
                      .HasColumnType("time")
                      .IsRequired();

                entity.Property(e => e.codigo)
                      .HasMaxLength(45);

                entity.Property(e => e.pagado)
                      .IsRequired();

                entity.Property(e => e.comercioRepartidor)
                      .IsRequired();

                entity.Property(e => e.subtotalPedido)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.createdAt)
                      .HasColumnType("datetime");

                entity.Property(e => e.updatedAt)
                      .HasColumnType("datetime");

                entity.Property(e => e.deletedAt)
                      .HasColumnType("datetime");

                // Relaciones
                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.ClienteIdCliente)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.EstadoPedido)
                      .WithMany()
                      .HasForeignKey(e => e.EstadoPedidoIdEstado)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MetodoPagoPedido)
                      .WithMany()
                      .HasForeignKey(e => e.MetodoPagoPedidoIdMetodo)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ItemsPedido)
                      .WithOne(i => i.Pedido)
                      .HasForeignKey(i => i.PedidoIdPedido)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --- Producto ---
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("producto");

                // Clave primaria
                entity.HasKey(e => e.idproducto);

                // Propiedades
                entity.Property(e => e.nombre)
                      .HasMaxLength(100);

                entity.Property(e => e.fotoPortada)
                      .HasMaxLength(255);

                entity.Property(e => e.descripcion)
                      .HasMaxLength(500);

                entity.Property(e => e.unidadMedida)
                      .HasMaxLength(50);

                entity.Property(e => e.precioUnitario)
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(e => e.oferta)
                      .HasDefaultValue(false);

                entity.Property(e => e.stock)
                      .HasDefaultValue(true);

                entity.Property(e => e.createdAt)
                      .HasColumnType("datetime");

                entity.Property(e => e.updatedAt)
                      .HasColumnType("datetime");

                entity.Property(e => e.deletedAt)
                      .HasColumnType("datetime");
            });

            // --- Horarios ---
            modelBuilder.Entity<Horarios>(entity =>
            {
                entity.ToTable("horarios");

                entity.HasKey(e => e.idhorarios);

                entity.Property(e => e.idhorarios)
                    .HasColumnName("idhorarios")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.apertura)
                    .HasColumnName("apertura")
                    .HasColumnType("time");

                entity.Property(e => e.cierre)
                    .HasColumnName("cierre")
                    .HasColumnType("time");

                entity.Property(e => e.dias)
                    .HasColumnName("dias")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.abierto)
                    .HasColumnName("abierto")
                    .IsRequired();

                entity.Property(e => e.createdAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.deletedAt)
                    .HasColumnName("deletedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.updatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

            });


            // --- CategoriaProducto (muchos a muchos) ---
            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.ToTable("categoria_has_producto");

                // Clave compuesta (muchos a muchos)
                entity.HasKey(e => new { e.CategoriaIdCategoria, e.ProductoIdProducto });

                entity.Property(e => e.CategoriaIdCategoria)
                    .HasColumnName("categoria_idcategoria");

                entity.Property(e => e.ProductoIdProducto)
                    .HasColumnName("producto_idproducto");

            });


            // --- Comercio ---
            modelBuilder.Entity<Comercio>(entity =>
            {
                entity.ToTable("comercio");

                entity.HasKey(e => e.idcomercio);

                entity.Property(e => e.idcomercio)
                    .HasColumnName("idcomercio")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.nombreComercio)
                    .HasColumnName("nombreComercio")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.tipoComercio)
                    .HasColumnName("tipoComercio")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.eslogan)
                    .HasColumnName("eslogan")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.fotoPortada)
                    .HasColumnName("fotoPortada")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.envio)
                    .HasColumnName("envio")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.deliveryPropio)
                    .HasColumnName("deliveryPropio");

                entity.Property(e => e.celular)
                    .HasColumnName("celular")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.calle)
                    .HasColumnName("calle")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.numero)
                    .HasColumnName("numero");

                entity.Property(e => e.sucursales)
                    .HasColumnName("sucursales");

                entity.Property(e => e.latitud)
                    .HasColumnName("latitud")
                    .HasColumnType("decimal(10,6)");

                entity.Property(e => e.longitud)
                    .HasColumnName("longitud")
                    .HasColumnType("decimal(10,6)");

                entity.Property(e => e.encargado)
                    .HasColumnName("encargado")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.cvu)
                    .HasColumnName("cvu")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.alias)
                    .HasColumnName("alias")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.destacado)
                    .HasColumnName("destacado")
                    .IsRequired();

                entity.Property(e => e.comision)
                    .HasColumnName("comision")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.createdAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.deletedAt)
                    .HasColumnName("deletedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.updatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

            });


            // --- ComercioCategoria (muchos a muchos) ---
            modelBuilder.Entity<ComercioCategoria>(entity =>
            {
                entity.ToTable("comercio_has_categoria");

                // Clave compuesta
                entity.HasKey(e => new { e.ComercioIdComercio, e.CategoriaIdCategoria });

                entity.Property(e => e.ComercioIdComercio)
                    .HasColumnName("comercio_idcomercio");

                entity.Property(e => e.CategoriaIdCategoria)
                    .HasColumnName("categoria_idcategoria");

            });

            // --- ComercioHorario (muchos a muchos) ---
            modelBuilder.Entity<ComercioHorario>(entity =>
            {
                entity.ToTable("comercio_has_horarios");

                // Clave compuesta
                entity.HasKey(e => new { e.ComercioIdComercio, e.HorariosIdHorarios });

                // Propiedades
                entity.Property(e => e.ComercioIdComercio)
                    .HasColumnName("comercio_idcomercio");

                entity.Property(e => e.HorariosIdHorarios)
                    .HasColumnName("horarios_idhorarios");

            });


            // --- ItemPedido ---
            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.ToTable("item_pedido");

                // Clave primaria
                entity.HasKey(e => e.iditemPedido);

                // Propiedades
                entity.Property(e => e.iditemPedido)
                      .HasColumnName("iditemPedido");

                entity.Property(e => e.cantProducto)
                      .IsRequired();

                entity.Property(e => e.precioFinal)
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(e => e.total)
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(e => e.createdAt)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                entity.Property(e => e.updatedAt)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                entity.Property(e => e.deletedAt)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                // Relaciones
                entity.HasOne(e => e.Producto)
                      .WithMany()
                      .HasForeignKey(e => e.ProductoIdProducto)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Pedido)
                      .WithMany(p => p.ItemsPedido)
                      .HasForeignKey(e => e.PedidoIdPedido)
                      .OnDelete(DeleteBehavior.Cascade);

            });


            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.ToTable("estado_pedido");

                // Clave primaria
                entity.HasKey(e => e.idestado);

                // Propiedades
                entity.Property(e => e.idestado)
                    .HasColumnName("idestado");

                entity.Property(e => e.tipo)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.createdAt)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.updatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.deletedAt)
                    .HasColumnType("datetime")
                    .IsRequired(false);
            });

            modelBuilder.Entity<MetodoPagoPedido>(entity =>
            {
                entity.ToTable("metodo_pago_pedido");

                // Clave primaria
                entity.HasKey(e => e.idmetodo);

                // Propiedades
                entity.Property(e => e.idmetodo)
                      .HasColumnName("idmetodo");

                entity.Property(e => e.metodo)
                      .IsRequired()
                      .HasMaxLength(45);

                entity.Property(e => e.createdAt)
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.updatedAt)
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.deletedAt)
                      .HasColumnType("datetime")
                      .IsRequired(false);
            });

        }
    }
}
