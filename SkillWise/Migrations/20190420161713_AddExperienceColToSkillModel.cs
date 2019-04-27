using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillWise.Migrations
{
    public partial class AddExperienceColToSkillModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Skills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Skills");
        }
    }
}
