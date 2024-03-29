using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Game_Design_DB.Migrations
{
    /// <inheritdoc />
    public partial class Simplify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Developer_DeveloperID",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Developer_DeveloperID",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Engine");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropIndex(
                name: "IX_Person_DeveloperID",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeveloperID",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "DeveloperID",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Game",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperID",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperID",
                table: "Game",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Engine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeveloperID = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Engine_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_DeveloperID",
                table: "Person",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperID",
                table: "Game",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Engine_DeveloperID",
                table: "Engine",
                column: "DeveloperID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Developer_DeveloperID",
                table: "Game",
                column: "DeveloperID",
                principalTable: "Developer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Developer_DeveloperID",
                table: "Person",
                column: "DeveloperID",
                principalTable: "Developer",
                principalColumn: "ID");
        }
    }
}
