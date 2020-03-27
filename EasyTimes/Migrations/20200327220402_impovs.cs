using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyTimes.Migrations
{
    public partial class impovs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "ServiceOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "ServiceOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "ServiceOrder");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "ServiceOrder");
        }
    }
}
