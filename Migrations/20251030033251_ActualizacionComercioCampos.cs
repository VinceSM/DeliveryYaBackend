using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryYaBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionComercioCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoria_has_producto_categoria_Categoriaidcategoria",
                table: "categoria_has_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_categoria_has_producto_categoria_categoria_idcategoria",
                table: "categoria_has_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_categoria_has_producto_producto_producto_idproducto",
                table: "categoria_has_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_categoria_categoria_Categoriaidcategoria",
                table: "comercio_has_categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_categoria_categoria_categoria_idcategoria",
                table: "comercio_has_categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_categoria_comercio_comercio_idcomercio",
                table: "comercio_has_categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_horarios_comercio_comercio_idcomercio",
                table: "comercio_has_horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_horarios_horarios_Horariosidhorarios",
                table: "comercio_has_horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_horarios_horarios_horarios_idhorarios",
                table: "comercio_has_horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_comercio_Comercioidcomercio",
                table: "ItemPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_pedido_PedidoIdPedido",
                table: "ItemPedidos");

            migrationBuilder.DropIndex(
                name: "IX_comercio_has_categoria_Categoriaidcategoria",
                table: "comercio_has_categoria");

            migrationBuilder.DropIndex(
                name: "IX_categoria_has_producto_Categoriaidcategoria",
                table: "categoria_has_producto");

            migrationBuilder.DropColumn(
                name: "Categoriaidcategoria",
                table: "comercio_has_categoria");

            migrationBuilder.DropColumn(
                name: "Categoriaidcategoria",
                table: "categoria_has_producto");

            migrationBuilder.AddColumn<bool>(
                name: "deliveryPropio",
                table: "comercio",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_has_producto_categoria_categoria_idcategoria",
                table: "categoria_has_producto",
                column: "categoria_idcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_has_producto_producto_producto_idproducto",
                table: "categoria_has_producto",
                column: "producto_idproducto",
                principalTable: "producto",
                principalColumn: "idproducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_categoria_categoria_categoria_idcategoria",
                table: "comercio_has_categoria",
                column: "categoria_idcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_categoria_comercio_comercio_idcomercio",
                table: "comercio_has_categoria",
                column: "comercio_idcomercio",
                principalTable: "comercio",
                principalColumn: "idcomercio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_horarios_comercio_comercio_idcomercio",
                table: "comercio_has_horarios",
                column: "comercio_idcomercio",
                principalTable: "comercio",
                principalColumn: "idcomercio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_horarios_horarios_Horariosidhorarios",
                table: "comercio_has_horarios",
                column: "Horariosidhorarios",
                principalTable: "horarios",
                principalColumn: "idhorarios");

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_horarios_horarios_horarios_idhorarios",
                table: "comercio_has_horarios",
                column: "horarios_idhorarios",
                principalTable: "horarios",
                principalColumn: "idhorarios",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_comercio_Comercioidcomercio",
                table: "ItemPedidos",
                column: "Comercioidcomercio",
                principalTable: "comercio",
                principalColumn: "idcomercio");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_pedido_PedidoIdPedido",
                table: "ItemPedidos",
                column: "PedidoIdPedido",
                principalTable: "pedido",
                principalColumn: "idpedido",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoria_has_producto_categoria_categoria_idcategoria",
                table: "categoria_has_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_categoria_has_producto_producto_producto_idproducto",
                table: "categoria_has_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_categoria_categoria_categoria_idcategoria",
                table: "comercio_has_categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_categoria_comercio_comercio_idcomercio",
                table: "comercio_has_categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_horarios_comercio_comercio_idcomercio",
                table: "comercio_has_horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_horarios_horarios_Horariosidhorarios",
                table: "comercio_has_horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comercio_has_horarios_horarios_horarios_idhorarios",
                table: "comercio_has_horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_comercio_Comercioidcomercio",
                table: "ItemPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_pedido_PedidoIdPedido",
                table: "ItemPedidos");

            migrationBuilder.DropColumn(
                name: "deliveryPropio",
                table: "comercio");

            migrationBuilder.AddColumn<int>(
                name: "Categoriaidcategoria",
                table: "comercio_has_categoria",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Categoriaidcategoria",
                table: "categoria_has_producto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comercio_has_categoria_Categoriaidcategoria",
                table: "comercio_has_categoria",
                column: "Categoriaidcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_has_producto_Categoriaidcategoria",
                table: "categoria_has_producto",
                column: "Categoriaidcategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_has_producto_categoria_Categoriaidcategoria",
                table: "categoria_has_producto",
                column: "Categoriaidcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_has_producto_categoria_categoria_idcategoria",
                table: "categoria_has_producto",
                column: "categoria_idcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_has_producto_producto_producto_idproducto",
                table: "categoria_has_producto",
                column: "producto_idproducto",
                principalTable: "producto",
                principalColumn: "idproducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_categoria_categoria_Categoriaidcategoria",
                table: "comercio_has_categoria",
                column: "Categoriaidcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_categoria_categoria_categoria_idcategoria",
                table: "comercio_has_categoria",
                column: "categoria_idcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_categoria_comercio_comercio_idcomercio",
                table: "comercio_has_categoria",
                column: "comercio_idcomercio",
                principalTable: "comercio",
                principalColumn: "idcomercio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_horarios_comercio_comercio_idcomercio",
                table: "comercio_has_horarios",
                column: "comercio_idcomercio",
                principalTable: "comercio",
                principalColumn: "idcomercio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_horarios_horarios_Horariosidhorarios",
                table: "comercio_has_horarios",
                column: "Horariosidhorarios",
                principalTable: "horarios",
                principalColumn: "idhorarios",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comercio_has_horarios_horarios_horarios_idhorarios",
                table: "comercio_has_horarios",
                column: "horarios_idhorarios",
                principalTable: "horarios",
                principalColumn: "idhorarios",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_comercio_Comercioidcomercio",
                table: "ItemPedidos",
                column: "Comercioidcomercio",
                principalTable: "comercio",
                principalColumn: "idcomercio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_pedido_PedidoIdPedido",
                table: "ItemPedidos",
                column: "PedidoIdPedido",
                principalTable: "pedido",
                principalColumn: "idpedido",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
