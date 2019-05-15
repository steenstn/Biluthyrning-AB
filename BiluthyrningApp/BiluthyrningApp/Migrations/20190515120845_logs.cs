using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningApp.Migrations
{
    public partial class logs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarLicensPlate",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Logs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarLicensPlate",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Logs");
        }
    }
}
