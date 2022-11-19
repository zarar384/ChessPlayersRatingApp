using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessPlayersRatingApp.Migrations
{
    public partial class InfoPlayerIdFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Information_InformationId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_InformationId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Information_PlayerId",
                table: "Information",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Information_Players_PlayerId",
                table: "Information",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Information_Players_PlayerId",
                table: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Information_PlayerId",
                table: "Information");

            migrationBuilder.CreateIndex(
                name: "IX_Players_InformationId",
                table: "Players",
                column: "InformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Information_InformationId",
                table: "Players",
                column: "InformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
