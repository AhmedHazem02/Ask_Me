using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AskMe.Migrations
{
    public partial class AddingAnonymouseinQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnonymouseQuestion",
                table: "Question",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnonymouseQuestion",
                table: "Question");
        }
    }
}
