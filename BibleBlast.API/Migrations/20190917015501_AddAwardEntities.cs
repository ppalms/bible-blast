using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibleBlast.API.Migrations
{
    public partial class AddAwardEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AwardItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Award",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    IsImmediate = table.Column<bool>(nullable: false),
                    Ordinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Award_MemoryCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MemoryCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Award_AwardItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "AwardItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AwardMemory",
                columns: table => new
                {
                    AwardId = table.Column<int>(nullable: false),
                    MemoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardMemory", x => new { x.AwardId, x.MemoryId });
                    table.ForeignKey(
                        name: "FK_AwardMemory_Award_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Award",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardMemory_Memory_MemoryId",
                        column: x => x.MemoryId,
                        principalTable: "Memory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Award_CategoryId",
                table: "Award",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Award_ItemId",
                table: "Award",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardMemory_MemoryId",
                table: "AwardMemory",
                column: "MemoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardMemory");

            migrationBuilder.DropTable(
                name: "Award");

            migrationBuilder.DropTable(
                name: "AwardItem");
        }
    }
}
