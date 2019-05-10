using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningApp.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedService",
                table: "Cars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TimeBooked",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedService",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TimeBooked",
                table: "Cars");
        }
    }
}
