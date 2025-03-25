using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerIDAndStatusMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3394bd62-c0c9-4ea8-92da-0d40d1e9303f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47d5890a-d381-4b43-a11c-3d03f84c5da7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdd76482-3ac1-4931-97a7-31fbbdca8c5c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08c91f1f-e00f-498b-a7aa-1d34f6f0e1f7", null, "Broker", "BROKER" },
                    { "cf6d99ac-bb79-41e7-8eb5-9a076378bf25", null, "Agent", "AGENT" },
                    { "f90ad76d-22e5-4afc-b8a6-5d2d90cde67c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08c91f1f-e00f-498b-a7aa-1d34f6f0e1f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf6d99ac-bb79-41e7-8eb5-9a076378bf25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f90ad76d-22e5-4afc-b8a6-5d2d90cde67c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3394bd62-c0c9-4ea8-92da-0d40d1e9303f", null, "Broker", "BROKER" },
                    { "47d5890a-d381-4b43-a11c-3d03f84c5da7", null, "Agent", "AGENT" },
                    { "fdd76482-3ac1-4931-97a7-31fbbdca8c5c", null, "Admin", "ADMIN" }
                });
        }
    }
}
