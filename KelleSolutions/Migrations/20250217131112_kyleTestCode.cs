using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class kyleTestCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "422a2516-0cc8-4596-a024-8193bee7687a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72f3d80c-94aa-40f6-b4bf-a425f5fed939");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c853a50f-b497-4b2e-8058-9fc46defaf90");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ee01d8b-32e8-4308-b5b3-ba140a796dcd", null, "Admin", "Admin" },
                    { "b613267a-4cb4-43ec-adfa-4b42ccc7fde6", null, "Broker", "Broker" },
                    { "c46b2537-733a-42f4-9268-47cfc3592b4b", null, "Agent", "Agent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ee01d8b-32e8-4308-b5b3-ba140a796dcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b613267a-4cb4-43ec-adfa-4b42ccc7fde6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c46b2537-733a-42f4-9268-47cfc3592b4b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "422a2516-0cc8-4596-a024-8193bee7687a", null, "Broker", "Broker" },
                    { "72f3d80c-94aa-40f6-b4bf-a425f5fed939", null, "Admin", "Admin" },
                    { "c853a50f-b497-4b2e-8058-9fc46defaf90", null, "Agent", "Agent" }
                });
        }
    }
}
