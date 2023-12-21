using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaxType_instances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "TaxTypes");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TaxTypes");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "TaxTypes");

            migrationBuilder.CreateTable(
                name: "DbTaxTypeInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Percentage = table.Column<float>(type: "real", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DbTaxTypeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTaxTypeInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbTaxTypeInstance_TaxTypes_DbTaxTypeId",
                        column: x => x.DbTaxTypeId,
                        principalTable: "TaxTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbTaxTypeInstance_DbTaxTypeId",
                table: "DbTaxTypeInstance",
                column: "DbTaxTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbTaxTypeInstance");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "TaxTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TaxTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Percentage",
                table: "TaxTypes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
