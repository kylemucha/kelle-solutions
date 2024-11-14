using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAffiliateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "Layout",
                table: "Affiliates");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Affiliates",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Affiliates",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Affiliates",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Affiliates",
                newName: "PostalCode");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Affiliates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Affiliates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Layout",
                table: "Affiliates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
