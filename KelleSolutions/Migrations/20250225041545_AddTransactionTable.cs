using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Tenant_TenantID",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ee4b4ed-89de-4574-aa75-b7823d35b11d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8f64e79-d4a7-4f99-a8ff-96ba8c1249c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7e85d3c-fb39-4a56-91b7-f699daffa2ec");

            migrationBuilder.RenameTable(
                name: "Tenant",
                newName: "Tenants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "TenantID");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppraisalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransitionDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fd95e75-6bc1-4012-954b-cce988fce40e", null, "Broker", "BROKER" },
                    { "21bb05d0-7a82-492a-84be-b0e217bf2cf2", null, "Admin", "ADMIN" },
                    { "a5960b24-c2ba-4b47-afd2-e8fe4022ef4b", null, "Agent", "AGENT" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenants_TenantID",
                table: "AspNetUsers",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Tenants_TenantID",
                table: "Properties",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenants_TenantID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Tenants_TenantID",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fd95e75-6bc1-4012-954b-cce988fce40e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21bb05d0-7a82-492a-84be-b0e217bf2cf2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5960b24-c2ba-4b47-afd2-e8fe4022ef4b");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "Tenant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "TenantID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ee4b4ed-89de-4574-aa75-b7823d35b11d", null, "Admin", "ADMIN" },
                    { "b8f64e79-d4a7-4f99-a8ff-96ba8c1249c9", null, "Agent", "AGENT" },
                    { "e7e85d3c-fb39-4a56-91b7-f699daffa2ec", null, "Broker", "BROKER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantID",
                table: "AspNetUsers",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Tenant_TenantID",
                table: "Properties",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "TenantID");
        }
    }
}
