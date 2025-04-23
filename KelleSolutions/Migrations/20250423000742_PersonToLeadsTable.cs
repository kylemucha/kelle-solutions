using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class PersonToLeadsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Leads",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Leads",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "PersonToLeads",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deprecated = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<short>(type: "smallint", nullable: false),
                    Person = table.Column<int>(type: "int", nullable: false),
                    Lead = table.Column<int>(type: "int", nullable: false),
                    Relation = table.Column<short>(type: "smallint", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonToLeads", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PersonToLeads_Leads_Lead",
                        column: x => x.Lead,
                        principalTable: "Leads",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonToLeads_Person_Person",
                        column: x => x.Person,
                        principalTable: "Person",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonToLeads_Lead",
                table: "PersonToLeads",
                column: "Lead");

            migrationBuilder.CreateIndex(
                name: "IX_PersonToLeads_Person",
                table: "PersonToLeads",
                column: "Person");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonToLeads");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
