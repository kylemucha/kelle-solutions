using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class PermissionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e3708a0-977d-4512-b0b2-76059f42ae55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b6c8240-3705-4c47-b243-ddc877cec45b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78df1887-0f1c-42d6-8265-3daa1382b058");

            migrationBuilder.CreateTable(
                name: "PageAccess",
                columns: table => new
                {
                    PageAccessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageAccessName = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageAccess", x => x.PageAccessID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroup",
                columns: table => new
                {
                    PermissionGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionID = table.Column<int>(type: "int", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ParentGroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroup", x => x.PermissionGroupID);
                    table.ForeignKey(
                        name: "FK_PermissionGroup_PermissionGroup_ParentGroupID",
                        column: x => x.ParentGroupID,
                        principalTable: "PermissionGroup",
                        principalColumn: "PermissionGroupID");
                    table.ForeignKey(
                        name: "FK_PermissionGroup_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permissions",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionGroupEntity",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PermissionGroupID = table.Column<int>(type: "int", nullable: false),
                    PageAccessID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionGroupEntity", x => new { x.RoleID, x.PermissionGroupID, x.PageAccessID });
                    table.ForeignKey(
                        name: "FK_RolePermissionGroupEntity_PageAccess_PageAccessID",
                        column: x => x.PageAccessID,
                        principalTable: "PageAccess",
                        principalColumn: "PageAccessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionGroupEntity_PermissionGroup_PermissionGroupID",
                        column: x => x.PermissionGroupID,
                        principalTable: "PermissionGroup",
                        principalColumn: "PermissionGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionGroupEntity_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f1981f4-bc29-490f-a6e9-0f2599efeaa9", null, "Broker", "BROKER" },
                    { "37c91bd6-b112-4d27-9eea-23edd0cd79df", null, "Admin", "ADMIN" },
                    { "8f5865fc-0b5d-44bd-b799-19be643a7c2a", null, "Agent", "AGENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroup_ParentGroupID",
                table: "PermissionGroup",
                column: "ParentGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroup_PermissionID",
                table: "PermissionGroup",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionGroupEntity_PageAccessID",
                table: "RolePermissionGroupEntity",
                column: "PageAccessID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionGroupEntity_PermissionGroupID",
                table: "RolePermissionGroupEntity",
                column: "PermissionGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionGroupEntity");

            migrationBuilder.DropTable(
                name: "PageAccess");

            migrationBuilder.DropTable(
                name: "PermissionGroup");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Permissions");

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
                    { "2e3708a0-977d-4512-b0b2-76059f42ae55", null, "Admin", "ADMIN" },
                    { "4b6c8240-3705-4c47-b243-ddc877cec45b", null, "Agent", "AGENT" },
                    { "78df1887-0f1c-42d6-8265-3daa1382b058", null, "Broker", "BROKER" }
                });
        }
    }
}
