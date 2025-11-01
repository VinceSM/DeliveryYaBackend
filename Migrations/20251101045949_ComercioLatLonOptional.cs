using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class ComercioLatLonOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "longitud",
                table: "comercio",
                type: "decimal(10,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "latitud",
                table: "comercio",
                type: "decimal(10,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "longitud",
                table: "comercio",
                type: "decimal(10,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "latitud",
                table: "comercio",
                type: "decimal(10,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)",
                oldNullable: true);
        }
    }
}
