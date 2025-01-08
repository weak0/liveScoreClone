using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveScoreReporter.EFCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLineups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lineups_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "FixtureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineupPlayers",
                columns: table => new
                {
                    LineupsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineupPlayers", x => new { x.LineupsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_LineupPlayers_Lineups_LineupsId",
                        column: x => x.LineupsId,
                        principalTable: "Lineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineupPlayers_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineupPlayers_PlayersId",
                table: "LineupPlayers",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_GameId",
                table: "Lineups",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineupPlayers");

            migrationBuilder.DropTable(
                name: "Lineups");
        }
    }
}
