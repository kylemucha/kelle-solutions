using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class RolePermissionGroupEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1981f4-bc29-490f-a6e9-0f2599efeaa9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37c91bd6-b112-4d27-9eea-23edd0cd79df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f5865fc-0b5d-44bd-b799-19be643a7c2a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "393550de-aa50-4977-8805-b1a06ba2953f", null, "Admin", "ADMIN" },
                    { "3db53e57-bb5e-4a3b-8de4-95f4327fd1a1", null, "Agent", "AGENT" },
                    { "ea1c2680-0b98-4232-ba78-d71fe32b2ece", null, "Broker", "BROKER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "393550de-aa50-4977-8805-b1a06ba2953f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3db53e57-bb5e-4a3b-8de4-95f4327fd1a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea1c2680-0b98-4232-ba78-d71fe32b2ece");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f1981f4-bc29-490f-a6e9-0f2599efeaa9", null, "Broker", "BROKER" },
                    { "37c91bd6-b112-4d27-9eea-23edd0cd79df", null, "Admin", "ADMIN" },
                    { "8f5865fc-0b5d-44bd-b799-19be643a7c2a", null, "Agent", "AGENT" }
                });
        }
    }
}
