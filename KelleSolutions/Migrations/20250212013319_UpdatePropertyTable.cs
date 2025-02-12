using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "UnitNumber",
                table: "Properties",
                newName: "PropertyType");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Properties",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "DateListed",
                table: "Properties",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Bedrooms",
                table: "Properties",
                newName: "YearConstructed");

            migrationBuilder.RenameColumn(
                name: "Bathrooms",
                table: "Properties",
                newName: "PartialCount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Properties",
                newName: "GarageCount");

            migrationBuilder.DropColumn(
                name: "GarageCount",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "GarageCount",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyID",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BathCount",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedCount",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplex",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemodeled",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LandSize",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Properties",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyID",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "BathCount",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "BedCount",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsComplex",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsRemodeled",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LandSize",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "YearConstructed",
                table: "Properties",
                newName: "Bedrooms");

            migrationBuilder.RenameColumn(
                name: "PropertyType",
                table: "Properties",
                newName: "UnitNumber");

            migrationBuilder.RenameColumn(
                name: "PartialCount",
                table: "Properties",
                newName: "Bathrooms");

            migrationBuilder.RenameColumn(
                name: "GarageCount",
                table: "Properties",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Properties",
                newName: "DateListed");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Properties",
                newName: "StreetAddress");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyId");
        }
    }
}
