using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveScoreReporter.EFCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamLineupRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Lineups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_TeamId",
                table: "Lineups",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Teams_TeamId",
                table: "Lineups",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Teams_TeamId",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_TeamId",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Lineups");
        }
    }
}
