using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DroppedTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Water");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Water",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Water",
                newName: "Day");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Water",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
