using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaxType_instances_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbTaxTypeInstance_DbTaxType_DbTaxTypeId",
                table: "DbTaxTypeInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbTaxTypeInstance",
                table: "DbTaxTypeInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbTaxType",
                table: "DbTaxType");

            migrationBuilder.RenameTable(
                name: "DbTaxTypeInstance",
                newName: "TaxTypeInstances");

            migrationBuilder.RenameTable(
                name: "DbTaxType",
                newName: "TaxTypes");

            migrationBuilder.RenameIndex(
                name: "IX_DbTaxTypeInstance_DbTaxTypeId",
                table: "TaxTypeInstances",
                newName: "IX_TaxTypeInstances_DbTaxTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxTypeInstances",
                table: "TaxTypeInstances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxTypes",
                table: "TaxTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxTypeInstances_TaxTypes_DbTaxTypeId",
                table: "TaxTypeInstances",
                column: "DbTaxTypeId",
                principalTable: "TaxTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxTypeInstances_TaxTypes_DbTaxTypeId",
                table: "TaxTypeInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxTypes",
                table: "TaxTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxTypeInstances",
                table: "TaxTypeInstances");

            migrationBuilder.RenameTable(
                name: "TaxTypes",
                newName: "DbTaxType");

            migrationBuilder.RenameTable(
                name: "TaxTypeInstances",
                newName: "DbTaxTypeInstance");

            migrationBuilder.RenameIndex(
                name: "IX_TaxTypeInstances_DbTaxTypeId",
                table: "DbTaxTypeInstance",
                newName: "IX_DbTaxTypeInstance_DbTaxTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbTaxType",
                table: "DbTaxType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbTaxTypeInstance",
                table: "DbTaxTypeInstance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbTaxTypeInstance_DbTaxType_DbTaxTypeId",
                table: "DbTaxTypeInstance",
                column: "DbTaxTypeId",
                principalTable: "DbTaxType",
                principalColumn: "Id");
        }
    }
}
