using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class improvementinmodels6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MealTicket",
                table: "Owner",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NormalTime",
                table: "Owner",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "LittleTask",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "LittleTask",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Overtime",
                table: "LittleTask",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTicket",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "NormalTime",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "LittleTask");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "LittleTask");

            migrationBuilder.DropColumn(
                name: "Overtime",
                table: "LittleTask");
        }
    }
}
