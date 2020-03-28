using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class improvementinmodels3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Json",
                table: "LittleTask");

            migrationBuilder.AddColumn<double>(
                name: "BetweenBoth",
                table: "LittleTask",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "LittleTask",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "LittleTask",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetweenBoth",
                table: "LittleTask");

            migrationBuilder.DropColumn(
                name: "End",
                table: "LittleTask");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "LittleTask");

            migrationBuilder.AddColumn<string>(
                name: "Json",
                table: "LittleTask",
                nullable: true);
        }
    }
}
