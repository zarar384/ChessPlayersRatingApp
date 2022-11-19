using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessPlayersRatingApp.Migrations
{
    public partial class Info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InformationId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Information",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseInfoText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatsText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Information_InformationId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Players_InformationId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "InformationId",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
