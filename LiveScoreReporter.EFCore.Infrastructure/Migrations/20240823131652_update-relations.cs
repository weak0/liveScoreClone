using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveScoreReporter.EFCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Scores_ScoreId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ScoreId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ScoreId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ScoreId",
                table: "Games",
                column: "ScoreId",
                unique: true,
                filter: "[ScoreId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Scores_ScoreId",
                table: "Games",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Scores_ScoreId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ScoreId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Scores");

            migrationBuilder.AlterColumn<int>(
                name: "ScoreId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ScoreId",
                table: "Games",
                column: "ScoreId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Scores_ScoreId",
                table: "Games",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
