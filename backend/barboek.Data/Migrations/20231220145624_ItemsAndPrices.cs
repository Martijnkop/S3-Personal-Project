using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class ItemsAndPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Items_ItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Items_OldItemId",
                table: "Prices");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Items",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemCategoryId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TaxTypeId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DbPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PriceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DbItemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbPrice_Items_DbItemId",
                        column: x => x.DbItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DbPrice_PriceTypes_PriceTypeId",
                        column: x => x.PriceTypeId,
                        principalTable: "PriceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OldItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemCategoryId",
                table: "Items",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TaxTypeId",
                table: "Items",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPrice_DbItemId",
                table: "DbPrice",
                column: "DbItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPrice_PriceTypeId",
                table: "DbPrice",
                column: "PriceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemCategories_ItemCategoryId",
                table: "Items",
                column: "ItemCategoryId",
                principalTable: "ItemCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_TaxTypes_TaxTypeId",
                table: "Items",
                column: "TaxTypeId",
                principalTable: "TaxTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_OldItems_ItemId",
                table: "OrderItem",
                column: "ItemId",
                principalTable: "OldItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_OldItems_OldItemId",
                table: "Prices",
                column: "OldItemId",
                principalTable: "OldItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemCategories_ItemCategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_TaxTypes_TaxTypeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_OldItems_ItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_OldItems_OldItemId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "DbPrice");

            migrationBuilder.DropTable(
                name: "OldItems");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemCategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TaxTypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TaxTypeId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Items_ItemId",
                table: "OrderItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Items_OldItemId",
                table: "Prices",
                column: "OldItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
