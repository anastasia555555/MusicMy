using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMy.Migrations
{
    public partial class AddAuthorAndPlaylistPointTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "MusicVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Playlist",
                table: "MusicVideo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "MusicVideo");

            migrationBuilder.DropColumn(
                name: "Playlist",
                table: "MusicVideo");
        }
    }
}
