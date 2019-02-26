using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibleBlast.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Award",
                columns: table => new
                {
                    AwardID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.AwardID);
                });

            migrationBuilder.CreateTable(
                name: "AwardQuestion",
                columns: table => new
                {
                    AwardID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardQuestion", x => new { x.AwardID, x.QuestionID });
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    FamilyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DadName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MomName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DadCell = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    MomCell = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    HomePhone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Address1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Address2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    City = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    State = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Zip = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    NonParentName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    EmergencyPhone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 125, nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.FamilyID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(nullable: false),
                    FamilyID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => new { x.PaymentID, x.FamilyID });
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    KidID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    SubmittedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => new { x.KidID, x.QuestionID });
                });

            migrationBuilder.CreateTable(
                name: "QuizScore",
                columns: table => new
                {
                    KidID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Points = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizScore", x => new { x.KidID, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Points = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Question_Category",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kid",
                columns: table => new
                {
                    KidID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Birthday = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FamilyID = table.Column<int>(nullable: false),
                    IsMale = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kid", x => x.KidID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Kid_Family",
                        column: x => x.FamilyID,
                        principalTable: "Family",
                        principalColumn: "FamilyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FamiyIDX",
                table: "Family",
                column: "FamilyID")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Kid_FamilyID",
                table: "Kid",
                column: "FamilyID");

            migrationBuilder.CreateIndex(
                name: "KidIDX",
                table: "Kid",
                column: "KidID")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryID",
                table: "Question",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Award");

            migrationBuilder.DropTable(
                name: "AwardQuestion");

            migrationBuilder.DropTable(
                name: "Kid");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropTable(
                name: "QuizScore");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
