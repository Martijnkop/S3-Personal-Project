using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class PriceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_PriceTypes_OldPriceTypeId",
                table: "Prices");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_OldPriceTypes_OldPriceTypeId",
                table: "Prices",
                column: "OldPriceTypeId",
                principalTable: "OldPriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_OldPriceTypes_OldPriceTypeId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "OldPriceTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_PriceTypes_OldPriceTypeId",
                table: "Prices",
                column: "OldPriceTypeId",
                principalTable: "PriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
