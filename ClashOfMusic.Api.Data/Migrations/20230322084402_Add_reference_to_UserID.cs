using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfMusic.Api.Data.Migrations
{
    public partial class Add_reference_to_UserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayLists_AspNetUsers_UserId",
                table: "PlayLists");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PlayLists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayLists_AspNetUsers_UserId",
                table: "PlayLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayLists_AspNetUsers_UserId",
                table: "PlayLists");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PlayLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayLists_AspNetUsers_UserId",
                table: "PlayLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
