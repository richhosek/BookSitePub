using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class SectionsBlurbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blurbs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quote = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blurbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blurbs_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Headline = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blurbs_BookId",
                table: "Blurbs",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_BookId",
                table: "Sections",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blurbs");

            migrationBuilder.DropTable(
                name: "Sections");
        }
    }
}
