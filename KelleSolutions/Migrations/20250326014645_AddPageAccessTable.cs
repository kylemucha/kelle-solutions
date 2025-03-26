using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddPageAccessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21e89b11-1d0d-49cd-bd38-b2a9a7dba742");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8af10aaf-1bc1-4b6e-8619-40ef475e8dc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec6d2a74-1c7c-494f-823c-065bfa918e1b");

            migrationBuilder.AlterColumn<string>(
                name: "PageAccessName",
                table: "PageAccess",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                table: "PageAccess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "PageAccess",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PageAccess",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DefaultPermissionGroupID",
                table: "PageAccess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PageAccess",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PageAccess",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "Operator",
                table: "PageAccess",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "PageUrl",
                table: "PageAccess",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Team",
                table: "PageAccess",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "PageAccess",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Visibility",
                table: "PageAccess",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "239329a7-815c-45e0-bb96-28f1ce23a06b", null, "Broker", "BROKER" },
                    { "a2e18dbc-72a2-4120-84b7-f4597b8769f4", null, "Agent", "AGENT" },
                    { "f7050cd6-1bc0-43fd-9cab-136276b06ec3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageAccess_DefaultPermissionGroupID",
                table: "PageAccess",
                column: "DefaultPermissionGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_PageAccess_PermissionGroups_DefaultPermissionGroupID",
                table: "PageAccess",
                column: "DefaultPermissionGroupID",
                principalTable: "PermissionGroups",
                principalColumn: "PermissionGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageAccess_PermissionGroups_DefaultPermissionGroupID",
                table: "PageAccess");

            migrationBuilder.DropIndex(
                name: "IX_PageAccess_DefaultPermissionGroupID",
                table: "PageAccess");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "239329a7-815c-45e0-bb96-28f1ce23a06b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2e18dbc-72a2-4120-84b7-f4597b8769f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7050cd6-1bc0-43fd-9cab-136276b06ec3");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "DefaultPermissionGroupID",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "PageUrl",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "PageAccess");

            migrationBuilder.AlterColumn<string>(
                name: "PageAccessName",
                table: "PageAccess",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21e89b11-1d0d-49cd-bd38-b2a9a7dba742", null, "Admin", "ADMIN" },
                    { "8af10aaf-1bc1-4b6e-8619-40ef475e8dc8", null, "Agent", "AGENT" },
                    { "ec6d2a74-1c7c-494f-823c-065bfa918e1b", null, "Broker", "BROKER" }
                });
        }
    }
}
