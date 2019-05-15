using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningApp.Migrations
{
    public partial class isBookingActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBookingActive",
                table: "Bookings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBookingActive",
                table: "Bookings");
        }
    }
}
