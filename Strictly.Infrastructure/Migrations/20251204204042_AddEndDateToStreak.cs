using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strictly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEndDateToStreak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Streaks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Streaks");
        }
    }
}
