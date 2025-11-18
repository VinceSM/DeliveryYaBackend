using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class MetodoPagoPermitidoComercio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "direccionEnvio",
                table: "pedido",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "pagoEfectivo",
                table: "comercio",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "pagoTarjeta",
                table: "comercio",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "pagoTransferencia",
                table: "comercio",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "direccionEnvio",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "pagoEfectivo",
                table: "comercio");

            migrationBuilder.DropColumn(
                name: "pagoTarjeta",
                table: "comercio");

            migrationBuilder.DropColumn(
                name: "pagoTransferencia",
                table: "comercio");
        }
    }
}
