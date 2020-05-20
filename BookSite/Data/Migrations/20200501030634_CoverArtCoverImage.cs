using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class CoverArtCoverImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverArt_Books_BookId",
                table: "CoverArt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverArt",
                table: "CoverArt");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CoverArt");

            migrationBuilder.RenameTable(
                name: "CoverArt",
                newName: "Covers");

            migrationBuilder.RenameIndex(
                name: "IX_CoverArt_BookId",
                table: "Covers",
                newName: "IX_Covers_BookId");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Covers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Covers",
                table: "Covers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Covers_ImageId",
                table: "Covers",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_Books_BookId",
                table: "Covers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_Images_ImageId",
                table: "Covers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_Books_BookId",
                table: "Covers");

            migrationBuilder.DropForeignKey(
                name: "FK_Covers_Images_ImageId",
                table: "Covers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Covers",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_ImageId",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Covers");

            migrationBuilder.RenameTable(
                name: "Covers",
                newName: "CoverArt");

            migrationBuilder.RenameIndex(
                name: "IX_Covers_BookId",
                table: "CoverArt",
                newName: "IX_CoverArt_BookId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "CoverArt",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverArt",
                table: "CoverArt",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverArt_Books_BookId",
                table: "CoverArt",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
