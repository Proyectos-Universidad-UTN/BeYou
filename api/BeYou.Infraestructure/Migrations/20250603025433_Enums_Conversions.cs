using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeYou.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Enums_Conversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryProduct_Product",
                table: "InventoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_InventoryProduct_IdProducto",
                table: "InventoryProduct");

            migrationBuilder.DropColumn(
                name: "IdProducto",
                table: "InventoryProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryProduct_Product",
                table: "InventoryProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryProduct_Product",
                table: "InventoryProduct");

            migrationBuilder.AddColumn<long>(
                name: "IdProducto",
                table: "InventoryProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_IdProducto",
                table: "InventoryProduct",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryProduct_Product",
                table: "InventoryProduct",
                column: "IdProducto",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
