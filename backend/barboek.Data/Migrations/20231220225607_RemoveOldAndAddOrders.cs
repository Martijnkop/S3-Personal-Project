using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldAndAddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Accounts_AccountOrderedId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "OldPrices");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "OldPriceTypes");

            migrationBuilder.DropTable(
                name: "OldItems");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AccountOrderedId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AccountOrderedId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "DbOrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DbOrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbOrderItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbOrderItem_Orders_DbOrderId",
                        column: x => x.DbOrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbOrderItem_DbOrderId",
                table: "DbOrderItem",
                column: "DbOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DbOrderItem_ItemId",
                table: "DbOrderItem",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbOrderItem");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountOrderedId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "OldPriceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldPriceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldOrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_OldItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "OldItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OldOrderId",
                        column: x => x.OldOrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OldPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OldPriceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OldItemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OldPrices_OldItems_OldItemId",
                        column: x => x.OldItemId,
                        principalTable: "OldItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OldPrices_OldPriceTypes_OldPriceTypeId",
                        column: x => x.OldPriceTypeId,
                        principalTable: "OldPriceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountOrderedId",
                table: "Orders",
                column: "AccountOrderedId");

            migrationBuilder.CreateIndex(
                name: "IX_OldPrices_OldItemId",
                table: "OldPrices",
                column: "OldItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OldPrices_OldPriceTypeId",
                table: "OldPrices",
                column: "OldPriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OldOrderId",
                table: "OrderItem",
                column: "OldOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Accounts_AccountOrderedId",
                table: "Orders",
                column: "AccountOrderedId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
