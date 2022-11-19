using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessPlayersRatingApp.Migrations
{
    public partial class InfoPlayerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Information",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Information");
        }
    }
}
