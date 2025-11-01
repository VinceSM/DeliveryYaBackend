using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
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
                    nombre = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
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
                    nombreCompleto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    celular = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ciudad = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    calle = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
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
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombreComercio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipoComercio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    eslogan = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fotoPortada = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    envio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    deliveryPropio = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    celular = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ciudad = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    calle = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    sucursales = table.Column<int>(type: "int", nullable: false),
                    latitud = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    longitud = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    encargado = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cvu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    destacado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comercio", x => x.idcomercio);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estado_pedido",
                columns: table => new
                {
                    idestado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado_pedido", x => x.idestado);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "horarios",
                columns: table => new
                {
                    idhorarios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    apertura = table.Column<TimeSpan>(type: "time", nullable: true),
                    cierre = table.Column<TimeSpan>(type: "time", nullable: true),
                    dias = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abierto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios", x => x.idhorarios);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "metodo_pago_pedido",
                columns: table => new
                {
                    idmetodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metodo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metodo_pago_pedido", x => x.idmetodo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    idproducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fotoPortada = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidadMedida = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    oferta = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false),
                    stock = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.idproducto);
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
                    categoria_idcategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comercio_has_categoria", x => new { x.comercio_idcomercio, x.categoria_idcategoria });
                    table.ForeignKey(
                        name: "FK_comercio_has_categoria_categoria_categoria_idcategoria",
                        column: x => x.categoria_idcategoria,
                        principalTable: "categoria",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comercio_has_categoria_comercio_comercio_idcomercio",
                        column: x => x.comercio_idcomercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comercio_has_horarios",
                columns: table => new
                {
                    comercio_idcomercio = table.Column<int>(type: "int", nullable: false),
                    horarios_idhorarios = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comercio_has_horarios", x => new { x.comercio_idcomercio, x.horarios_idhorarios });
                    table.ForeignKey(
                        name: "FK_comercio_has_horarios_comercio_comercio_idcomercio",
                        column: x => x.comercio_idcomercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comercio_has_horarios_horarios_horarios_idhorarios",
                        column: x => x.horarios_idhorarios,
                        principalTable: "horarios",
                        principalColumn: "idhorarios",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    idpedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    codigo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pagado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    comercioRepartidor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    subtotalPedido = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    cliente_idcliente = table.Column<int>(type: "int", nullable: false),
                    estadopedido_idestado = table.Column<int>(type: "int", nullable: false),
                    metodoPagoPedido_idmetodo = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.idpedido);
                    table.ForeignKey(
                        name: "FK_pedido_cliente_cliente_idcliente",
                        column: x => x.cliente_idcliente,
                        principalTable: "cliente",
                        principalColumn: "idcliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_estado_pedido_estadopedido_idestado",
                        column: x => x.estadopedido_idestado,
                        principalTable: "estado_pedido",
                        principalColumn: "idestado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_metodo_pago_pedido_metodoPagoPedido_idmetodo",
                        column: x => x.metodoPagoPedido_idmetodo,
                        principalTable: "metodo_pago_pedido",
                        principalColumn: "idmetodo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria_has_producto",
                columns: table => new
                {
                    categoria_idcategoria = table.Column<int>(type: "int", nullable: false),
                    producto_idproducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_has_producto", x => new { x.categoria_idcategoria, x.producto_idproducto });
                    table.ForeignKey(
                        name: "FK_categoria_has_producto_categoria_categoria_idcategoria",
                        column: x => x.categoria_idcategoria,
                        principalTable: "categoria",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categoria_has_producto_producto_producto_idproducto",
                        column: x => x.producto_idproducto,
                        principalTable: "producto",
                        principalColumn: "idproducto",
                        onDelete: ReferentialAction.Cascade);
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
                name: "item_pedido",
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
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_pedido", x => x.iditemPedido);
                    table.ForeignKey(
                        name: "FK_item_pedido_comercio_ComercioIdComercio",
                        column: x => x.ComercioIdComercio,
                        principalTable: "comercio",
                        principalColumn: "idcomercio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_pedido_pedido_PedidoIdPedido",
                        column: x => x.PedidoIdPedido,
                        principalTable: "pedido",
                        principalColumn: "idpedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_pedido_producto_ProductoIdProducto",
                        column: x => x.ProductoIdProducto,
                        principalTable: "producto",
                        principalColumn: "idproducto",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_categoria_has_producto_producto_idproducto",
                table: "categoria_has_producto",
                column: "producto_idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_categoria_categoria_idcategoria",
                table: "comercio_has_categoria",
                column: "categoria_idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_horarios_horarios_idhorarios",
                table: "comercio_has_horarios",
                column: "horarios_idhorarios");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_ComercioIdComercio",
                table: "item_pedido",
                column: "ComercioIdComercio");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_PedidoIdPedido",
                table: "item_pedido",
                column: "PedidoIdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_ProductoIdProducto",
                table: "item_pedido",
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
                name: "IX_repartidor_vehiculoIdVehiculo",
                table: "repartidor",
                column: "vehiculoIdVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_stockIlimitado",
                table: "Stocks",
                column: "stockIlimitado");

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
                name: "item_pedido");

            migrationBuilder.DropTable(
                name: "Stocks");

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
                name: "repartidor");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "estado_pedido");

            migrationBuilder.DropTable(
                name: "metodo_pago_pedido");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
