using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibleBlast.API.Migrations
{
    public partial class AddKidAwardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KidAward",
                columns: table => new
                {
                    KidId = table.Column<int>(nullable: false),
                    AwardId = table.Column<int>(nullable: false),
                    DatePresented = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidAward", x => new { x.KidId, x.AwardId });
                    table.ForeignKey(
                        name: "FK_KidAward_Award_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Award",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KidAward_Kid_KidId",
                        column: x => x.KidId,
                        principalTable: "Kid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KidAward_AwardId",
                table: "KidAward",
                column: "AwardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KidAward");
        }
    }
}
