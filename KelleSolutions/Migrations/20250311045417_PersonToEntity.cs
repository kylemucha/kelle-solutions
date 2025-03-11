using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class PersonToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "111aab5a-0dde-42cd-b8aa-ef207eeea5da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c1b022f-ae6a-4334-a6c8-9b653cc3c4e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "616d8337-bd5d-42cd-bf93-a843978487d4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonToEntity",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deprecated = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<short>(type: "smallint", nullable: false),
                    Person = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<int>(type: "int", nullable: false),
                    Relation = table.Column<short>(type: "smallint", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonToEntity", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PersonToEntity_Entities_Entity",
                        column: x => x.Entity,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonToEntity_Person_Person",
                        column: x => x.Person,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e3708a0-977d-4512-b0b2-76059f42ae55", null, "Admin", "ADMIN" },
                    { "4b6c8240-3705-4c47-b243-ddc877cec45b", null, "Agent", "AGENT" },
                    { "78df1887-0f1c-42d6-8265-3daa1382b058", null, "Broker", "BROKER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonToEntity_Entity",
                table: "PersonToEntity",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PersonToEntity_Person",
                table: "PersonToEntity",
                column: "Person");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonToEntity");

            migrationBuilder.DropTable(
                name: "Person");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "111aab5a-0dde-42cd-b8aa-ef207eeea5da", null, "Broker", "BROKER" },
                    { "5c1b022f-ae6a-4334-a6c8-9b653cc3c4e9", null, "Agent", "AGENT" },
                    { "616d8337-bd5d-42cd-bf93-a843978487d4", null, "Admin", "ADMIN" }
                });
        }
    }
}
