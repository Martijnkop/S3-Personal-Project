using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class Order_PriceTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PriceTypeId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PriceTypeId",
                table: "Orders",
                column: "PriceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PriceTypes_PriceTypeId",
                table: "Orders",
                column: "PriceTypeId",
                principalTable: "PriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PriceTypes_PriceTypeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PriceTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceTypeId",
                table: "Orders");
        }
    }
}
