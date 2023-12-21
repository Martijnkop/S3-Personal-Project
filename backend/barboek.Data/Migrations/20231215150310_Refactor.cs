using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Items_ItemId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_PriceTypes_PriceTypeId",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "PriceTypeId",
                table: "Prices",
                newName: "OldPriceTypeId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Prices",
                newName: "OldItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_PriceTypeId",
                table: "Prices",
                newName: "IX_Prices_OldPriceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_ItemId",
                table: "Prices",
                newName: "IX_Prices_OldItemId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItem",
                newName: "OldOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OldOrderId");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OldOrderId",
                table: "OrderItem",
                column: "OldOrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Items_OldItemId",
                table: "Prices",
                column: "OldItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_PriceTypes_OldPriceTypeId",
                table: "Prices",
                column: "OldPriceTypeId",
                principalTable: "PriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OldOrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Items_OldItemId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_PriceTypes_OldPriceTypeId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "OldPriceTypeId",
                table: "Prices",
                newName: "PriceTypeId");

            migrationBuilder.RenameColumn(
                name: "OldItemId",
                table: "Prices",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_OldPriceTypeId",
                table: "Prices",
                newName: "IX_Prices_PriceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_OldItemId",
                table: "Prices",
                newName: "IX_Prices_ItemId");

            migrationBuilder.RenameColumn(
                name: "OldOrderId",
                table: "OrderItem",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OldOrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Items_ItemId",
                table: "Prices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_PriceTypes_PriceTypeId",
                table: "Prices",
                column: "PriceTypeId",
                principalTable: "PriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
