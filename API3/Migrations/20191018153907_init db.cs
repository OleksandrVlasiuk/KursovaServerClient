using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API3.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl.UsersAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl.UsersAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    UserAccount_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_tbl.UsersAccounts_UserAccount_id",
                        column: x => x.UserAccount_id,
                        principalTable: "tbl.UsersAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl.UsersLogIn",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl.UsersLogIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl.UsersLogIn_tbl.UsersAccounts_Id",
                        column: x => x.Id,
                        principalTable: "tbl.UsersAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl.Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    About = table.Column<int>(nullable: false),
                    UserAccount_id = table.Column<int>(nullable: false),
                    Friends_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl.Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl.Messages_Friends_Friends_Id",
                        column: x => x.Friends_Id,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl.Messages_tbl.UsersAccounts_UserAccount_id",
                        column: x => x.UserAccount_id,
                        principalTable: "tbl.UsersAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl.Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    File = table.Column<string>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    UserAccount_id = table.Column<int>(nullable: false),
                    Friends_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl.Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl.Posts_Friends_Friends_id",
                        column: x => x.Friends_id,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl.Posts_tbl.UsersAccounts_UserAccount_id",
                        column: x => x.UserAccount_id,
                        principalTable: "tbl.UsersAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl.Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Info = table.Column<string>(nullable: false),
                    FriendsId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl.Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl.Comments_Friends_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl.Comments_tbl.Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "tbl.Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserAccount_id",
                table: "Friends",
                column: "UserAccount_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl.Comments_FriendsId",
                table: "tbl.Comments",
                column: "FriendsId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl.Comments_PostId",
                table: "tbl.Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl.Messages_Friends_Id",
                table: "tbl.Messages",
                column: "Friends_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl.Messages_UserAccount_id",
                table: "tbl.Messages",
                column: "UserAccount_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl.Posts_Friends_id",
                table: "tbl.Posts",
                column: "Friends_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl.Posts_UserAccount_id",
                table: "tbl.Posts",
                column: "UserAccount_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl.Comments");

            migrationBuilder.DropTable(
                name: "tbl.Messages");

            migrationBuilder.DropTable(
                name: "tbl.UsersLogIn");

            migrationBuilder.DropTable(
                name: "tbl.Posts");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "tbl.UsersAccounts");
        }
    }
}
