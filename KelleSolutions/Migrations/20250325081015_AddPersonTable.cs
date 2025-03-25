using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KelleSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonToEntity_Person_Person",
                table: "PersonToEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToListing_Person_PersonId",
                table: "PersonToListing");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToPerson_Person_Person2Id",
                table: "PersonToPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToPerson_Person_PersonId",
                table: "PersonToPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToProperties_Person_Person",
                table: "PersonToProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerIDMappings_Person_PersonID",
                table: "SellerIDMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08c91f1f-e00f-498b-a7aa-1d34f6f0e1f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf6d99ac-bb79-41e7-8eb5-9a076378bf25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f90ad76d-22e5-4afc-b8a6-5d2d90cde67c");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "People");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Code");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c33e807-11ed-4bc2-8f66-a0b6d442febf", null, "Agent", "AGENT" },
                    { "621bf771-cd59-4223-a7f2-59de8e092fbc", null, "Admin", "ADMIN" },
                    { "89d5ab8f-78e1-4ad9-b571-35c7d051b2cf", null, "Broker", "BROKER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToEntity_People_Person",
                table: "PersonToEntity",
                column: "Person",
                principalTable: "People",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToListing_People_PersonId",
                table: "PersonToListing",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToPerson_People_Person2Id",
                table: "PersonToPerson",
                column: "Person2Id",
                principalTable: "People",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToPerson_People_PersonId",
                table: "PersonToPerson",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToProperties_People_Person",
                table: "PersonToProperties",
                column: "Person",
                principalTable: "People",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerIDMappings_People_PersonID",
                table: "SellerIDMappings",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonToEntity_People_Person",
                table: "PersonToEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToListing_People_PersonId",
                table: "PersonToListing");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToPerson_People_Person2Id",
                table: "PersonToPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToPerson_People_PersonId",
                table: "PersonToPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonToProperties_People_Person",
                table: "PersonToProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerIDMappings_People_PersonID",
                table: "SellerIDMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c33e807-11ed-4bc2-8f66-a0b6d442febf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "621bf771-cd59-4223-a7f2-59de8e092fbc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89d5ab8f-78e1-4ad9-b571-35c7d051b2cf");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Code");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08c91f1f-e00f-498b-a7aa-1d34f6f0e1f7", null, "Broker", "BROKER" },
                    { "cf6d99ac-bb79-41e7-8eb5-9a076378bf25", null, "Agent", "AGENT" },
                    { "f90ad76d-22e5-4afc-b8a6-5d2d90cde67c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToEntity_Person_Person",
                table: "PersonToEntity",
                column: "Person",
                principalTable: "Person",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToListing_Person_PersonId",
                table: "PersonToListing",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToPerson_Person_Person2Id",
                table: "PersonToPerson",
                column: "Person2Id",
                principalTable: "Person",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToPerson_Person_PersonId",
                table: "PersonToPerson",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonToProperties_Person_Person",
                table: "PersonToProperties",
                column: "Person",
                principalTable: "Person",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerIDMappings_Person_PersonID",
                table: "SellerIDMappings",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
