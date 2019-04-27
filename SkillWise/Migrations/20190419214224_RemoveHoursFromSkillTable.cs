using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillWise.Migrations
{
    public partial class RemoveHoursFromSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Skills");

            migrationBuilder.AddColumn<DateTime>(
                name: "_timestamp",
                table: "Skills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_timestamp",
                table: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "TotalHours",
                table: "Skills",
                nullable: true);
        }
    }
}
