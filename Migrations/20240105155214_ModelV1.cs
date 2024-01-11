using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentsApp.Migrations
{
    public partial class ModelV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_userId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Comment",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_userId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comment",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                newName: "IX_Comment_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_userId",
                table: "Comment",
                column: "userId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
