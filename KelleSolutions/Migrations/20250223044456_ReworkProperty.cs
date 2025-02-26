using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class ReworkProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "038a9a85-e0fb-4e7a-a063-02316bc8e087");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18a405d5-0305-4096-9e15-1621e1af9d30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "921d82d8-f538-4b32-9efe-29557ca8eea3");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "796c0283-6af9-4b10-a68d-08c29aa2042f", null, "Admin", "Admin" },
                    { "d95efda7-8b6c-4461-a331-d50683231e0c", null, "Broker", "Broker" },
                    { "eb50ff63-c869-4166-a8b6-e6503a9a5b5b", null, "Agent", "Agent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserID",
                table: "Properties",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_UserID",
                table: "Properties",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_UserID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UserID",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "796c0283-6af9-4b10-a68d-08c29aa2042f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d95efda7-8b6c-4461-a331-d50683231e0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb50ff63-c869-4166-a8b6-e6503a9a5b5b");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Properties");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "038a9a85-e0fb-4e7a-a063-02316bc8e087", null, "Admin", "Admin" },
                    { "18a405d5-0305-4096-9e15-1621e1af9d30", null, "Broker", "Broker" },
                    { "921d82d8-f538-4b32-9efe-29557ca8eea3", null, "Agent", "Agent" }
                });
        }
    }
}
