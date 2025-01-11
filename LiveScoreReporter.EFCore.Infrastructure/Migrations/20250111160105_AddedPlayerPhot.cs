using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveScoreReporter.EFCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlayerPhot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Players");
        }
    }
}
