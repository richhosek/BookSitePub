using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class BookCoverArt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CoverArt",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverArt",
                table: "Books");
        }
    }
}
