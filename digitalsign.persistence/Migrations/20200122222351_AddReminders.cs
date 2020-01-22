using Microsoft.EntityFrameworkCore.Migrations;

namespace digitalsign.persistence.Migrations
{
    public partial class AddReminders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Messages",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DayOfTheMonth",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DayOfTheMonth",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Messages");
        }
    }
}
