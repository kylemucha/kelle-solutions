using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44adf492-e1de-4c43-baef-6eb1db899afe", null, "Admin", "Admin" },
                    { "b78c8c85-e17e-4cb1-9d01-3bf2c68c0ff0", null, "Broker", "Broker" },
                    { "fdef5211-ac96-49eb-9025-75ec22931824", null, "Agent", "Agent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44adf492-e1de-4c43-baef-6eb1db899afe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b78c8c85-e17e-4cb1-9d01-3bf2c68c0ff0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdef5211-ac96-49eb-9025-75ec22931824");
        }
    }
}
