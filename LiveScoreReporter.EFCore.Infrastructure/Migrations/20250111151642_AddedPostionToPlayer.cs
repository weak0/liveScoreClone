using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveScoreReporter.EFCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostionToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Postition",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postition",
                table: "Players");
        }
    }
}
