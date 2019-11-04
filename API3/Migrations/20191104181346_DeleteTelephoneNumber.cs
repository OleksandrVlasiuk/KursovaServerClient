using Microsoft.EntityFrameworkCore.Migrations;

namespace API3.Migrations
{
    public partial class DeleteTelephoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }
    }
}
