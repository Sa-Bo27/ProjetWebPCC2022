using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoviesDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adult = table.Column<bool>(type: "INTEGER", nullable: false),
                    Backdrop_path = table.Column<string>(type: "TEXT", nullable: true),
                    Original_language = table.Column<string>(type: "TEXT", nullable: true),
                    Original_title = table.Column<string>(type: "TEXT", nullable: true),
                    Overview = table.Column<string>(type: "TEXT", nullable: true),
                    Popularity = table.Column<double>(type: "REAL", nullable: false),
                    Poster_path = table.Column<string>(type: "TEXT", nullable: true),
                    Release_date = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieDomUser",
                columns: table => new
                {
                    ListMoviesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDomUser", x => new { x.ListMoviesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_MovieDomUser_MoviesDm_ListMoviesId",
                        column: x => x.ListMoviesId,
                        principalTable: "MoviesDm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieDomUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1, "Bob01@test.com", "Bob" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieDomUser_UsersId",
                table: "MovieDomUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieDomUser");

            migrationBuilder.DropTable(
                name: "MoviesDm");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
