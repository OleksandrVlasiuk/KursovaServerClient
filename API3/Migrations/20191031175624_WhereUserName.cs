using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API3.Migrations
{
    public partial class WhereUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Comments_tbl.UsersAccounts_UserAccountId",
                table: "tbl.Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Friends_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Messages_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Posts_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.UserFriends_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.UserFriends");

            migrationBuilder.DropTable(
                name: "tbl.UsersAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Comments_AspNetUsers_UserAccountId",
                table: "tbl.Comments",
                column: "UserAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Friends_AspNetUsers_UserAccount_id",
                table: "tbl.Friends",
                column: "UserAccount_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Messages_AspNetUsers_UserAccount_id",
                table: "tbl.Messages",
                column: "UserAccount_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Posts_AspNetUsers_UserAccount_id",
                table: "tbl.Posts",
                column: "UserAccount_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.UserFriends_AspNetUsers_UserAccount_id",
                table: "tbl.UserFriends",
                column: "UserAccount_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Comments_AspNetUsers_UserAccountId",
                table: "tbl.Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Friends_AspNetUsers_UserAccount_id",
                table: "tbl.Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Messages_AspNetUsers_UserAccount_id",
                table: "tbl.Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.Posts_AspNetUsers_UserAccount_id",
                table: "tbl.Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl.UserFriends_AspNetUsers_UserAccount_id",
                table: "tbl.UserFriends");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "tbl.UsersAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl.UsersAccounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Comments_tbl.UsersAccounts_UserAccountId",
                table: "tbl.Comments",
                column: "UserAccountId",
                principalTable: "tbl.UsersAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Friends_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.Friends",
                column: "UserAccount_id",
                principalTable: "tbl.UsersAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Messages_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.Messages",
                column: "UserAccount_id",
                principalTable: "tbl.UsersAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.Posts_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.Posts",
                column: "UserAccount_id",
                principalTable: "tbl.UsersAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl.UserFriends_tbl.UsersAccounts_UserAccount_id",
                table: "tbl.UserFriends",
                column: "UserAccount_id",
                principalTable: "tbl.UsersAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
