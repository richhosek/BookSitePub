using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSite.Data.Migrations
{
    public partial class MakeSeriesTextFieldOfBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Series_SeriesId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Books_SeriesId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Series",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_SeriesId",
                table: "Books",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Series_SeriesId",
                table: "Books",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
