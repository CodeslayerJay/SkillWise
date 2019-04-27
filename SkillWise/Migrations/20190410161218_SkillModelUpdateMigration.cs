using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillWise.Migrations
{
    public partial class SkillModelUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationActual",
                table: "SkillTasks");

            migrationBuilder.DropColumn(
                name: "DurationEstimated",
                table: "SkillTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Skills",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Skills",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalHours",
                table: "Skills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "DurationActual",
                table: "SkillTasks",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DurationEstimated",
                table: "SkillTasks",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Skills",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Skills",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
