using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentsApp.Migrations
{
    public partial class subComments4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SubComments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SubComments");
        }
    }
}
