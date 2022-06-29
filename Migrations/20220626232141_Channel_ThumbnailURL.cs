using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMy.Migrations
{
    public partial class Channel_ThumbnailURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "MusicVideo",
                newName: "Channel");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalId",
                table: "MusicVideo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Channel",
                table: "MusicVideo",
                newName: "Author");

            migrationBuilder.AlterColumn<int>(
                name: "OriginalId",
                table: "MusicVideo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
