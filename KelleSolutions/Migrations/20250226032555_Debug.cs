using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class Debug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_AgentID",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_UserID",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_UserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_UserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_UserID",
                table: "Listings");

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

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Listings",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0972d5e7-4667-4068-939d-b68e0ff1ba62", null, "Admin", "ADMIN" },
                    { "b7aeb529-0a21-40eb-b969-852f37e49f29", null, "Broker", "BROKER" },
                    { "f63fb8f1-3e24-4034-bfa8-9db2b7266b96", null, "Agent", "AGENT" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_AgentID",
                table: "Listings",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_AgentID",
                table: "Listings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0972d5e7-4667-4068-939d-b68e0ff1ba62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7aeb529-0a21-40eb-b969-852f37e49f29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f63fb8f1-3e24-4034-bfa8-9db2b7266b96");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Listings",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Listings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Listings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "422a2516-0cc8-4596-a024-8193bee7687a", null, "Broker", "Broker" },
                    { "72f3d80c-94aa-40f6-b4bf-a425f5fed939", null, "Admin", "Admin" },
                    { "c853a50f-b497-4b2e-8058-9fc46defaf90", null, "Agent", "Agent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserId",
                table: "Listings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserID",
                table: "Listings",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_AgentID",
                table: "Listings",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_UserID",
                table: "Listings",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_UserId",
                table: "Listings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
