using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Game_Design_DB.Migrations
{
    /// <inheritdoc />
    public partial class PersonalWebsites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlePerson");

            migrationBuilder.DropTable(
                name: "BookPerson");

            migrationBuilder.DropTable(
                name: "PersonPodcast");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Podcast");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperID",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceID",
                table: "Game",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Engine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true),
                    DeveloperID = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PersonalWebsite",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    URL = table.Column<string>(type: "text", nullable: false),
                    PersonID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalWebsite", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonalWebsite_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersonResource",
                columns: table => new
                {
                    AuthorsID = table.Column<int>(type: "integer", nullable: false),
                    ResourcesID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonResource", x => new { x.AuthorsID, x.ResourcesID });
                    table.ForeignKey(
                        name: "FK_PersonResource_Person_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonResource_Resource_ResourcesID",
                        column: x => x.ResourcesID,
                        principalTable: "Resource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_DeveloperID",
                table: "Person",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_ResourceID",
                table: "Game",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Engine_DeveloperID",
                table: "Engine",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalWebsite_PersonID",
                table: "PersonalWebsite",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonResource_ResourcesID",
                table: "PersonResource",
                column: "ResourcesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Resource_ResourceID",
                table: "Game",
                column: "ResourceID",
                principalTable: "Resource",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Developer_DeveloperID",
                table: "Person",
                column: "DeveloperID",
                principalTable: "Developer",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Resource_ResourceID",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Developer_DeveloperID",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Engine");

            migrationBuilder.DropTable(
                name: "PersonalWebsite");

            migrationBuilder.DropTable(
                name: "PersonResource");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Person_DeveloperID",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Game_ResourceID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeveloperID",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ResourceID",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ISBN = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Podcast",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podcast", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ArticlePerson",
                columns: table => new
                {
                    ArticlesID = table.Column<int>(type: "integer", nullable: false),
                    AuthorsID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePerson", x => new { x.ArticlesID, x.AuthorsID });
                    table.ForeignKey(
                        name: "FK_ArticlePerson_Article_ArticlesID",
                        column: x => x.ArticlesID,
                        principalTable: "Article",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePerson_Person_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookPerson",
                columns: table => new
                {
                    AuthorsID = table.Column<int>(type: "integer", nullable: false),
                    BooksID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPerson", x => new { x.AuthorsID, x.BooksID });
                    table.ForeignKey(
                        name: "FK_BookPerson_Book_BooksID",
                        column: x => x.BooksID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPerson_Person_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPodcast",
                columns: table => new
                {
                    PodcastsID = table.Column<int>(type: "integer", nullable: false),
                    SpeakersID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPodcast", x => new { x.PodcastsID, x.SpeakersID });
                    table.ForeignKey(
                        name: "FK_PersonPodcast_Person_SpeakersID",
                        column: x => x.SpeakersID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPodcast_Podcast_PodcastsID",
                        column: x => x.PodcastsID,
                        principalTable: "Podcast",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePerson_AuthorsID",
                table: "ArticlePerson",
                column: "AuthorsID");

            migrationBuilder.CreateIndex(
                name: "IX_BookPerson_BooksID",
                table: "BookPerson",
                column: "BooksID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPodcast_SpeakersID",
                table: "PersonPodcast",
                column: "SpeakersID");
        }
    }
}
