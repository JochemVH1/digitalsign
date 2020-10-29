using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace digitalsign.persistence.Migrations
{
    public partial class AddTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CompletedUserId = table.Column<Guid>(nullable: true),
                    MessageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.id);
                    table.ForeignKey(
                        name: "FK_Task_Users_CompletedUserId",
                        column: x => x.CompletedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_CompletedUserId",
                table: "Task",
                column: "CompletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatedUserId",
                table: "Task",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_MessageId",
                table: "Task",
                column: "MessageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
