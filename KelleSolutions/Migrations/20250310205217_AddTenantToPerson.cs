using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e872660-9e54-4fc1-9e45-d0e0d8e4ea58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f938015-2c0d-4fbf-bd94-c9fd19487a03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ba7e85a-dd15-4b0c-9045-e7141d4b942c");

            migrationBuilder.CreateTable(
                name: "TenantToPeople",
                columns: table => new
                {
                    TenantID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantToPeople", x => new { x.TenantID, x.PersonID });
                    table.ForeignKey(
                        name: "FK_TenantToPeople_AspNetUsers_PersonID",
                        column: x => x.PersonID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantToPeople_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "533c04e1-bd00-4559-b7bd-4a5a6dd19e66", null, "Admin", "ADMIN" },
                    { "c0302cec-1682-44a8-b8aa-fb592cf5a71f", null, "Broker", "BROKER" },
                    { "de1704c5-85f6-434a-9f15-9a788da29cc6", null, "Agent", "AGENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantToPeople_PersonID",
                table: "TenantToPeople",
                column: "PersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantToPeople");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "533c04e1-bd00-4559-b7bd-4a5a6dd19e66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0302cec-1682-44a8-b8aa-fb592cf5a71f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1704c5-85f6-434a-9f15-9a788da29cc6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e872660-9e54-4fc1-9e45-d0e0d8e4ea58", null, "Agent", "AGENT" },
                    { "1f938015-2c0d-4fbf-bd94-c9fd19487a03", null, "Admin", "ADMIN" },
                    { "6ba7e85a-dd15-4b0c-9045-e7141d4b942c", null, "Broker", "BROKER" }
                });
        }
    }
}
