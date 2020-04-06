using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class sdsdsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "End_string",
                table: "LittleTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Start_string",
                table: "LittleTask",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End_string",
                table: "LittleTask");

            migrationBuilder.DropColumn(
                name: "Start_string",
                table: "LittleTask");
        }
    }
}
