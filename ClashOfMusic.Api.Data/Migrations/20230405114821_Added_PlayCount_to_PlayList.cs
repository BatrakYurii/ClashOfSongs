using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfMusic.Api.Data.Migrations
{
    public partial class Added_PlayCount_to_PlayList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayCount",
                table: "PlayLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayCount",
                table: "PlayLists");
        }
    }
}
