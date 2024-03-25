using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_Design_DB.Migrations
{
    /// <inheritdoc />
    public partial class URLsToSingleWebsite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URLs",
                table: "Podcast");

            migrationBuilder.DropColumn(
                name: "URLs",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "URLs",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "URLs",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "URLs",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Podcast",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Game",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Book",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Podcast");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Article");

            migrationBuilder.AddColumn<List<string>>(
                name: "URLs",
                table: "Podcast",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "URLs",
                table: "Person",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "URLs",
                table: "Game",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "URLs",
                table: "Book",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "URLs",
                table: "Article",
                type: "text[]",
                nullable: false);
        }
    }
}
