using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedWaterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Day",
                table: "Water",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "LoggedTime",
                table: "Water",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Water");

            migrationBuilder.DropColumn(
                name: "LoggedTime",
                table: "Water");
        }
    }
}
