using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaxType_instances_FromCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbTaxTypeInstance_TaxTypes_DbTaxTypeId",
                table: "DbTaxTypeInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxTypes",
                table: "TaxTypes");

            migrationBuilder.RenameTable(
                name: "TaxTypes",
                newName: "DbTaxType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbTaxType",
                table: "DbTaxType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbTaxTypeInstance_DbTaxType_DbTaxTypeId",
                table: "DbTaxTypeInstance",
                column: "DbTaxTypeId",
                principalTable: "DbTaxType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbTaxTypeInstance_DbTaxType_DbTaxTypeId",
                table: "DbTaxTypeInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbTaxType",
                table: "DbTaxType");

            migrationBuilder.RenameTable(
                name: "DbTaxType",
                newName: "TaxTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxTypes",
                table: "TaxTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbTaxTypeInstance_TaxTypes_DbTaxTypeId",
                table: "DbTaxTypeInstance",
                column: "DbTaxTypeId",
                principalTable: "TaxTypes",
                principalColumn: "Id");
        }
    }
}
