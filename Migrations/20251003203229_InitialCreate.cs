using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    idadmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.idadmin);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    idcategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.idcategoria);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    idcliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombreCompleto = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    celular = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ciudad = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    calle = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.idcliente);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comercio",
                columns: table => new
                {
                    idcomercio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombreComercio = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fotoPortada = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    celular = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ciudad = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    calle = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    latitud = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    longitud = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    encargado = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cvu = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    destacado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comercio", x => x.idcomercio);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstadoPedidos",
                columns: table => new
                {
                    idestado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPedidos", x => x.idestado);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "horarios",
                columns: table => new
                {
                    idhorarios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    apertura = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    cierre = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    dias = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abierto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios", x => x.idhorarios);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MetodoPagoPedidos",
                columns: table => new
                {
                    idmetodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metodo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagoPedidos", x => x.idmetodo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    idstock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stock = table.Column<int>(type: "int", nullable: false),
                    stockIlimitado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    medida = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.idstock);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    idvehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    marca = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modelo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    patente = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    seguro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    companiaSeguros = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.idvehiculo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comercio_has_categoria",
                columns: table => new
                {
                    comercio_idcomercio = table.Column<int>(type: "int", nullable: false),
                    categoria_idcategoria = table.Column<int>(type: "int", nullable: false),
                    Categoriaidcategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comercio_has_categoria", x => new { x.comercio_idcomercio, x.categoria_idcategoria });
                    table.ForeignKey(
                        name: "FK_comercio_has_categoria_categoria_Categoriaidcategoria",
                        column: x => x.Categoriaidcategoria,
                        principalTable: "categoria",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comercio_has_categoria_categoria_categoria_idcategoria",
                        column: x => x.categoria_idcategoria,
                        principalTable: "categoria",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comercio_has_categoria_comercio_comercio_idcomercio",
                        column: x => x.comercio_idcomercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comercio_has_horarios",
                columns: table => new
                {
                    comercio_idcomercio = table.Column<int>(type: "int", nullable: false),
                    horarios_idhorarios = table.Column<int>(type: "int", nullable: false),
                    Horariosidhorarios = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comercio_has_horarios", x => new { x.comercio_idcomercio, x.horarios_idhorarios });
                    table.ForeignKey(
                        name: "FK_comercio_has_horarios_comercio_comercio_idcomercio",
                        column: x => x.comercio_idcomercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comercio_has_horarios_horarios_Horariosidhorarios",
                        column: x => x.Horariosidhorarios,
                        principalTable: "horarios",
                        principalColumn: "idhorarios",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comercio_has_horarios_horarios_horarios_idhorarios",
                        column: x => x.horarios_idhorarios,
                        principalTable: "horarios",
                        principalColumn: "idhorarios",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    idproducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fotoPortada = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidadMedida = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    oferta = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    stock_idstock = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.idproducto);
                    table.ForeignKey(
                        name: "FK_producto_Stocks_stock_idstock",
                        column: x => x.stock_idstock,
                        principalTable: "Stocks",
                        principalColumn: "idstock",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "repartidor",
                columns: table => new
                {
                    idrepartidor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombreCompleto = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    celular = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ciudad = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    calle = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantPedidos = table.Column<int>(type: "int", nullable: false),
                    puntuacion = table.Column<decimal>(type: "decimal(10,1)", nullable: false),
                    cvu = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    libreRepartidor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    vehiculoIdVehiculo = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repartidor", x => x.idrepartidor);
                    table.ForeignKey(
                        name: "FK_repartidor_Vehiculos_vehiculoIdVehiculo",
                        column: x => x.vehiculoIdVehiculo,
                        principalTable: "Vehiculos",
                        principalColumn: "idvehiculo",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria_has_producto",
                columns: table => new
                {
                    categoria_idcategoria = table.Column<int>(type: "int", nullable: false),
                    producto_idproducto = table.Column<int>(type: "int", nullable: false),
                    Categoriaidcategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_has_producto", x => new { x.categoria_idcategoria, x.producto_idproducto });
                    table.ForeignKey(
                        name: "FK_categoria_has_producto_categoria_Categoriaidcategoria",
                        column: x => x.Categoriaidcategoria,
                        principalTable: "categoria",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_categoria_has_producto_categoria_categoria_idcategoria",
                        column: x => x.categoria_idcategoria,
                        principalTable: "categoria",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_categoria_has_producto_producto_producto_idproducto",
                        column: x => x.producto_idproducto,
                        principalTable: "producto",
                        principalColumn: "idproducto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    idpedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hora = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    codigo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pagado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    comercioRepartidor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    subtotalPedido = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    cliente_idcliente = table.Column<int>(type: "int", nullable: false),
                    repartidor_idrepartidor = table.Column<int>(type: "int", nullable: false),
                    estadopedido_idestado = table.Column<int>(type: "int", nullable: false),
                    metodoPagoPedido_idmetodo = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.idpedido);
                    table.ForeignKey(
                        name: "FK_pedido_EstadoPedidos_estadopedido_idestado",
                        column: x => x.estadopedido_idestado,
                        principalTable: "EstadoPedidos",
                        principalColumn: "idestado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedido_MetodoPagoPedidos_metodoPagoPedido_idmetodo",
                        column: x => x.metodoPagoPedido_idmetodo,
                        principalTable: "MetodoPagoPedidos",
                        principalColumn: "idmetodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedido_cliente_cliente_idcliente",
                        column: x => x.cliente_idcliente,
                        principalTable: "cliente",
                        principalColumn: "idcliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedido_repartidor_repartidor_idrepartidor",
                        column: x => x.repartidor_idrepartidor,
                        principalTable: "repartidor",
                        principalColumn: "idrepartidor",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tarifaRepartidorLibre",
                columns: table => new
                {
                    idtarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cantPedidos = table.Column<int>(type: "int", nullable: false),
                    tarifaBase = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    kmRecorridos = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    repartidor_idrepartidor = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarifaRepartidorLibre", x => x.idtarifa);
                    table.ForeignKey(
                        name: "FK_tarifaRepartidorLibre_repartidor_repartidor_idrepartidor",
                        column: x => x.repartidor_idrepartidor,
                        principalTable: "repartidor",
                        principalColumn: "idrepartidor",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemPedidos",
                columns: table => new
                {
                    iditemPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cantProducto = table.Column<int>(type: "int", nullable: false),
                    precioFinal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProductoIdProducto = table.Column<int>(type: "int", nullable: false),
                    PedidoIdPedido = table.Column<int>(type: "int", nullable: false),
                    ComercioIdComercio = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Comercioidcomercio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidos", x => x.iditemPedido);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_comercio_ComercioIdComercio",
                        column: x => x.ComercioIdComercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_comercio_Comercioidcomercio",
                        column: x => x.Comercioidcomercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_pedido_PedidoIdPedido",
                        column: x => x.PedidoIdPedido,
                        principalTable: "pedido",
                        principalColumn: "idpedido",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_producto_ProductoIdProducto",
                        column: x => x.ProductoIdProducto,
                        principalTable: "producto",
                        principalColumn: "idproducto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_has_producto_Categoriaidcategoria",
                table: "categoria_has_producto",
                column: "Categoriaidcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_has_producto_producto_idproducto",
                table: "categoria_has_producto",
                column: "producto_idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_categoria_categoria_idcategoria",
                table: "comercio_has_categoria",
                column: "categoria_idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_categoria_Categoriaidcategoria",
                table: "comercio_has_categoria",
                column: "Categoriaidcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_horarios_horarios_idhorarios",
                table: "comercio_has_horarios",
                column: "horarios_idhorarios");

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_horarios_Horariosidhorarios",
                table: "comercio_has_horarios",
                column: "Horariosidhorarios");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_Comercioidcomercio",
                table: "ItemPedidos",
                column: "Comercioidcomercio");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ComercioIdComercio",
                table: "ItemPedidos",
                column: "ComercioIdComercio");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoIdPedido",
                table: "ItemPedidos",
                column: "PedidoIdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ProductoIdProducto",
                table: "ItemPedidos",
                column: "ProductoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_cliente_idcliente",
                table: "pedido",
                column: "cliente_idcliente");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_estadopedido_idestado",
                table: "pedido",
                column: "estadopedido_idestado");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_metodoPagoPedido_idmetodo",
                table: "pedido",
                column: "metodoPagoPedido_idmetodo");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_repartidor_idrepartidor",
                table: "pedido",
                column: "repartidor_idrepartidor");

            migrationBuilder.CreateIndex(
                name: "IX_producto_stock_idstock",
                table: "producto",
                column: "stock_idstock");

            migrationBuilder.CreateIndex(
                name: "IX_repartidor_vehiculoIdVehiculo",
                table: "repartidor",
                column: "vehiculoIdVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_tarifaRepartidorLibre_repartidor_idrepartidor",
                table: "tarifaRepartidorLibre",
                column: "repartidor_idrepartidor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "categoria_has_producto");

            migrationBuilder.DropTable(
                name: "comercio_has_categoria");

            migrationBuilder.DropTable(
                name: "comercio_has_horarios");

            migrationBuilder.DropTable(
                name: "ItemPedidos");

            migrationBuilder.DropTable(
                name: "tarifaRepartidorLibre");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "horarios");

            migrationBuilder.DropTable(
                name: "comercio");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "EstadoPedidos");

            migrationBuilder.DropTable(
                name: "MetodoPagoPedidos");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "repartidor");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
