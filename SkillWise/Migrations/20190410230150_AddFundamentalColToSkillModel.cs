using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillWise.Migrations
{
    public partial class AddFundamentalColToSkillModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFundamental",
                table: "SkillTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFundamental",
                table: "SkillTasks");
        }
    }
}
