using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voter.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Mail = table.Column<string>(maxLength: 64, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Country = table.Column<string>(maxLength: 32, nullable: false),
                    Sex = table.Column<string>(maxLength: 8, nullable: false),
                    HashedPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    PollId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.PollId);
                    table.ForeignKey(
                        name: "FK_Polls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 512, nullable: false),
                    PollId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "PollId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VariantId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(maxLength: 256, nullable: false),
                    QuestionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_Variants_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerAggregates",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    VariantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerAggregates", x => new { x.UserId, x.QuestionId, x.VariantId });
                    table.ForeignKey(
                        name: "FK_AnswerAggregates_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerAggregates_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerAggregates_QuestionId",
                table: "AnswerAggregates",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerAggregates_VariantId",
                table: "AnswerAggregates",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_UserId",
                table: "Polls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PollId",
                table: "Questions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_QuestionId",
                table: "Variants",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerAggregates");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
