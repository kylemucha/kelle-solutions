using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class kyleCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "5e3cbea3-6cf9-4727-a5ac-e9da4c8eec51", null, "Admin", "Admin" },
                    { "6be6b3ce-5c0e-4bbc-ad88-94ced8a6bdc1", null, "Agent", "Agent" },
                    { "d31981aa-e4f1-402a-be18-0c637c85ed27", null, "Broker", "Broker" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e3cbea3-6cf9-4727-a5ac-e9da4c8eec51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6be6b3ce-5c0e-4bbc-ad88-94ced8a6bdc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d31981aa-e4f1-402a-be18-0c637c85ed27");

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
    }
}
