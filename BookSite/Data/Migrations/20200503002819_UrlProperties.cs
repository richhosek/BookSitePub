using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class UrlProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleUrl",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PenNameUrl",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PenNameUrl",
                table: "Authors");
        }
    }
}
