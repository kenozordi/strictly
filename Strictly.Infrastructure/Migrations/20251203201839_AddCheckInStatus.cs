using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strictly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckInStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "CheckIns");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckedInAt",
                table: "CheckIns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "CheckIns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CheckIns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CheckIns",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CheckedInAt",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CheckIns");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CheckIns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
