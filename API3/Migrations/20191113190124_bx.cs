using Microsoft.EntityFrameworkCore.Migrations;

namespace API3.Migrations
{
    public partial class bx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyComment",
                table: "tbl.Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyComment",
                table: "tbl.Posts");
        }
    }
}
