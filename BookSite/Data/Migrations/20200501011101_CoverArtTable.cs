using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class CoverArtTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverArt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverArtFileType",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverArtSpineWidth",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "CoverArt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    SpineWidth = table.Column<float>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverArt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoverArt_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoverArt_BookId",
                table: "CoverArt",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverArt");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverArt",
                table: "Books",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverArtFileType",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CoverArtSpineWidth",
                table: "Books",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
