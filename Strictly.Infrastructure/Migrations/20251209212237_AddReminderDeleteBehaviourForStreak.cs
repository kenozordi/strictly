using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strictly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderDeleteBehaviourForStreak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Streaks_StreakId",
                table: "Reminder");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Streaks_StreakId",
                table: "Reminder",
                column: "StreakId",
                principalTable: "Streaks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Streaks_StreakId",
                table: "Reminder");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Streaks_StreakId",
                table: "Reminder",
                column: "StreakId",
                principalTable: "Streaks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
