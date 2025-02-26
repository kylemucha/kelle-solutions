using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class ReworkListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "870cad46-5012-426c-9e59-c0c97b5d0861");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb47bebf-1fd6-447c-a619-6e33cddaea5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdb01e27-653b-44c4-b8af-6667eea45d1e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "870cad46-5012-426c-9e59-c0c97b5d0861", null, "Admin", "Admin" },
                    { "bb47bebf-1fd6-447c-a619-6e33cddaea5b", null, "Agent", "Agent" },
                    { "cdb01e27-653b-44c4-b8af-6667eea45d1e", null, "Broker", "Broker" }
                });
        }
    }
}
