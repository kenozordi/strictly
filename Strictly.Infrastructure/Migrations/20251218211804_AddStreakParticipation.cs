using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strictly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStreakParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StreakParticipant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreakId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreakParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreakParticipant_Streaks_StreakId",
                        column: x => x.StreakId,
                        principalTable: "Streaks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StreakParticipant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StreakParticipant_StreakId",
                table: "StreakParticipant",
                column: "StreakId");

            migrationBuilder.CreateIndex(
                name: "IX_StreakParticipant_UserId",
                table: "StreakParticipant",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreakParticipant");
        }
    }
}
