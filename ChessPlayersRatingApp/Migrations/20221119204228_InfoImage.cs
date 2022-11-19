using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessPlayersRatingApp.Migrations
{
    public partial class InfoImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailedText",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "StatsText",
                table: "Information");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Information",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Information");

            migrationBuilder.AddColumn<string>(
                name: "DetailedText",
                table: "Information",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatsText",
                table: "Information",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
