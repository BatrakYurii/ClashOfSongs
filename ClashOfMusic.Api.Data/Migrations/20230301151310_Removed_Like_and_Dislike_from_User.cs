using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfMusic.Api.Data.Migrations
{
    public partial class Removed_Like_and_Dislike_from_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislike",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
