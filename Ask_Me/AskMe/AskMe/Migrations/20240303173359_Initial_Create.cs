using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AskMe.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Unkown = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id_Question = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreadId_Question = table.Column<int>(type: "int", nullable: true),
                    From_ID = table.Column<int>(type: "int", nullable: false),
                    To_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id_Question);
                    table.ForeignKey(
                        name: "FK_Question_Question_ThreadId_Question",
                        column: x => x.ThreadId_Question,
                        principalTable: "Question",
                        principalColumn: "Id_Question");
                    table.ForeignKey(
                        name: "FK_Question_Users_From_ID",
                        column: x => x.From_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Users_To_ID",
                        column: x => x.To_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_From_ID",
                table: "Question",
                column: "From_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ThreadId_Question",
                table: "Question",
                column: "ThreadId_Question");

            migrationBuilder.CreateIndex(
                name: "IX_Question_To_ID",
                table: "Question",
                column: "To_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
