using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strictly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Users_UserId",
                table: "Reminder");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Users_UserId",
                table: "Reminder",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Users_UserId",
                table: "Reminder");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Users_UserId",
                table: "Reminder",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
