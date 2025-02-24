using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateKyle : Migration
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
                    { "4324e76c-7df5-4be8-94d6-69cf8bb51df4", null, "Admin", "Admin" },
                    { "bd830206-42ae-4ed6-8f24-39c53968e0c8", null, "Broker", "Broker" },
                    { "f963a999-d18e-4ee5-a772-e25d8bb50af9", null, "Agent", "Agent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4324e76c-7df5-4be8-94d6-69cf8bb51df4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd830206-42ae-4ed6-8f24-39c53968e0c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f963a999-d18e-4ee5-a772-e25d8bb50af9");

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
