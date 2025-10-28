using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionProductoComercio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_producto_Stocks_stock_idstock",
                table: "producto");

            migrationBuilder.DropIndex(
                name: "IX_producto_stock_idstock",
                table: "producto");

            migrationBuilder.DropColumn(
                name: "stock_idstock",
                table: "producto");

            migrationBuilder.AddColumn<bool>(
                name: "stock",
                table: "producto",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "comercio",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "envio",
                table: "comercio",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_stockIlimitado",
                table: "Stocks",
                column: "stockIlimitado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_stockIlimitado",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "producto");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "comercio");

            migrationBuilder.DropColumn(
                name: "envio",
                table: "comercio");

            migrationBuilder.AddColumn<int>(
                name: "stock_idstock",
                table: "producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_producto_stock_idstock",
                table: "producto",
                column: "stock_idstock");

            migrationBuilder.AddForeignKey(
                name: "FK_producto_Stocks_stock_idstock",
                table: "producto",
                column: "stock_idstock",
                principalTable: "Stocks",
                principalColumn: "idstock",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
