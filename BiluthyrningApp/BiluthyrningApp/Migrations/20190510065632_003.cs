using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningApp.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedsCleaning",
                table: "Cars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedsCleaning",
                table: "Cars");
        }
    }
}
