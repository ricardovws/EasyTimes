using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class improvementinmodels14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MealTicketValue",
                table: "ServiceOrder",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealTicketValue",
                table: "ServiceOrder");
        }
    }
}
