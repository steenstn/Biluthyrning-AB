using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningApp.Migrations
{
    public partial class CarId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarLicensPlate",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Logs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "CarLicensPlate",
                table: "Logs",
                nullable: true);
        }
    }
}
