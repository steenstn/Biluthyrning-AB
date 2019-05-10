using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningApp.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CarRemoved",
                table: "Cars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarRemoved",
                table: "Cars");
        }
    }
}
