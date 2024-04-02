using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_Design_DB.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Resource_ResourceID",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_ResourceID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ResourceID",
                table: "Game");

            migrationBuilder.CreateTable(
                name: "GameResource",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "integer", nullable: false),
                    ResourcesID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResource", x => new { x.GamesID, x.ResourcesID });
                    table.ForeignKey(
                        name: "FK_GameResource_Game_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameResource_Resource_ResourcesID",
                        column: x => x.ResourcesID,
                        principalTable: "Resource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameResource_ResourcesID",
                table: "GameResource",
                column: "ResourcesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameResource");

            migrationBuilder.AddColumn<int>(
                name: "ResourceID",
                table: "Game",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_ResourceID",
                table: "Game",
                column: "ResourceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Resource_ResourceID",
                table: "Game",
                column: "ResourceID",
                principalTable: "Resource",
                principalColumn: "ID");
        }
    }
}
