using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class improvementinmodels12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NormalHours",
                table: "ServiceOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Overtime",
                table: "ServiceOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OvertimeValue",
                table: "LittleTask",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalHours",
                table: "ServiceOrder");

            migrationBuilder.DropColumn(
                name: "Overtime",
                table: "ServiceOrder");

            migrationBuilder.DropColumn(
                name: "OvertimeValue",
                table: "LittleTask");
        }
    }
}
