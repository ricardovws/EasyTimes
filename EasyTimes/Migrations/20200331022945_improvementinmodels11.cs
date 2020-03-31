using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class improvementinmodels11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTicket",
                table: "ServiceOrder");

            migrationBuilder.DropColumn(
                name: "Overtime",
                table: "ServiceOrder");

            migrationBuilder.AlterColumn<bool>(
                name: "Overtime",
                table: "LittleTask",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<bool>(
                name: "MealTicket",
                table: "LittleTask",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTicket",
                table: "LittleTask");

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

            migrationBuilder.AlterColumn<double>(
                name: "Overtime",
                table: "LittleTask",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
