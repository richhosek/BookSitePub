using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class CoverArtImageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "CoverArt",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "CoverArt");
        }
    }
}
