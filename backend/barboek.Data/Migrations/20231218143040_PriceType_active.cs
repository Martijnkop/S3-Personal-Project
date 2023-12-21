using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barboek.Data.Migrations
{
    /// <inheritdoc />
    public partial class PriceType_active : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "PriceTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "PriceTypes");
        }
    }
}
