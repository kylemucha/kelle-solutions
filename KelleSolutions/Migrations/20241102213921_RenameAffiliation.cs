using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class RenameAffiliation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Affiliate",
                table: "AspNetUsers",
                newName: "Affiliation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Affiliation",
                table: "AspNetUsers",
                newName: "Affiliate");
        }
    }
}
