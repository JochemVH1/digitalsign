using Microsoft.EntityFrameworkCore.Migrations;

namespace digitalsign_api.Migrations
{
    public partial class Message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Guid = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {   
                    table.PrimaryKey("PK_Messages", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
