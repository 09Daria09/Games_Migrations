using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesContextLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddGameModes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameModeId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameMode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMode", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameModeId",
                table: "Games",
                column: "GameModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameMode_GameModeId",
                table: "Games",
                column: "GameModeId",
                principalTable: "GameMode",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameMode_GameModeId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameMode");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameModeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameModeId",
                table: "Games");
        }
    }
}
