using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class SectionColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headline",
                table: "Sections");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BackgroundImage",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sections");

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
