using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class improvementinmodels10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MealTicket",
                table: "ServiceOrder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Overtime",
                table: "ServiceOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTicket",
                table: "ServiceOrder");

            migrationBuilder.DropColumn(
                name: "Overtime",
                table: "ServiceOrder");
        }
    }
}
