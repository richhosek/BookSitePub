using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class SectionImageSeparation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Sections");

            migrationBuilder.AddColumn<int>(
                name: "BackgroundImageId",
                table: "Sections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_BackgroundImageId",
                table: "Sections",
                column: "BackgroundImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Images_BackgroundImageId",
                table: "Sections",
                column: "BackgroundImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Images_BackgroundImageId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_BackgroundImageId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "BackgroundImageId",
                table: "Sections");

            migrationBuilder.AddColumn<byte[]>(
                name: "BackgroundImage",
                table: "Sections",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
