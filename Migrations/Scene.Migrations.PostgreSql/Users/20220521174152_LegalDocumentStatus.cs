using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scene.Migrations.PostgreSql.Users
{
    public partial class LegalDocumentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LegalDocuments");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LegalDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LegalDocuments");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LegalDocuments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
