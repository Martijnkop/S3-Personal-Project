using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class PricesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_OldItems_OldItemId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_OldPriceTypes_OldPriceTypeId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "DbPrice");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "OldPriceTypeId",
                table: "Prices",
                newName: "PriceTypeId");

            migrationBuilder.RenameColumn(
                name: "OldItemId",
                table: "Prices",
                newName: "DbItemId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Prices",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "BeginTime",
                table: "Prices",
                newName: "StartTime");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_OldPriceTypeId",
                table: "Prices",
                newName: "IX_Prices_PriceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_OldItemId",
                table: "Prices",
                newName: "IX_Prices_DbItemId");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Prices",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "OldPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OldPriceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "IX_OldPrices_OldItemId",
                table: "OldPrices",
                column: "OldItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OldPrices_OldPriceTypeId",
                table: "OldPrices",
                column: "OldPriceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Items_DbItemId",
                table: "Prices",
                column: "DbItemId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Items_DbItemId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_PriceTypes_PriceTypeId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "OldPrices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Prices",
                newName: "BeginTime");

            migrationBuilder.RenameColumn(
                name: "PriceTypeId",
                table: "Prices",
                newName: "OldPriceTypeId");

            migrationBuilder.RenameColumn(
                name: "DbItemId",
                table: "Prices",
                newName: "OldItemId");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Prices",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_PriceTypeId",
                table: "Prices",
                newName: "IX_Prices_OldPriceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_DbItemId",
                table: "Prices",
                newName: "IX_Prices_OldItemId");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Prices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Prices",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DbItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_DbPrice_DbItemId",
                table: "DbPrice",
                column: "DbItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPrice_PriceTypeId",
                table: "DbPrice",
                column: "PriceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_OldItems_OldItemId",
                table: "Prices",
                column: "OldItemId",
                principalTable: "OldItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_OldPriceTypes_OldPriceTypeId",
                table: "Prices",
                column: "OldPriceTypeId",
                principalTable: "OldPriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
