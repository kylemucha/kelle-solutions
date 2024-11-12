using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Properties",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Properties",
                newName: "UnitNumber");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Properties",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UnitNumber",
                table: "Properties",
                newName: "Location");
        }
    }
}
