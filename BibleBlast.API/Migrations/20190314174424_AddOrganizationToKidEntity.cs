using Microsoft.EntityFrameworkCore.Migrations;

namespace BibleBlast.API.Migrations
{
    public partial class AddOrganizationToKidEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Kid",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Kid_OrganizationId",
                table: "Kid",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kid_Organization_OrganizationId",
                table: "Kid",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kid_Organization_OrganizationId",
                table: "Kid");

            migrationBuilder.DropIndex(
                name: "IX_Kid_OrganizationId",
                table: "Kid");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Kid");
        }
    }
}
