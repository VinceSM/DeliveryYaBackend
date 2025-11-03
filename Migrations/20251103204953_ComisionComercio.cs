using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class ComisionComercio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "comision",
                table: "comercio",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comision",
                table: "comercio");
        }
    }
}
