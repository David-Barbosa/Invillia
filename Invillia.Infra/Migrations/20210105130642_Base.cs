using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invillia.Infra.Migrations
{
    public partial class Base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(fixedLength: true, maxLength: 32, nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "friend",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    cellPhone = table.Column<string>(maxLength: 11, nullable: false),
                    id_user = table.Column<Guid>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    exclude = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friend", x => x.id);
                    table.ForeignKey(
                        name: "FK_friend_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    available = table.Column<bool>(nullable: false),
                    id_user = table.Column<Guid>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    exclude = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loan",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    id_user = table.Column<Guid>(nullable: false),
                    id_game = table.Column<Guid>(nullable: false),
                    id_friend = table.Column<Guid>(nullable: false),
                    loan_date = table.Column<DateTime>(nullable: false),
                    return_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan", x => x.id);
                    table.ForeignKey(
                        name: "FK_loan_friend_id_friend",
                        column: x => x.id_friend,
                        principalTable: "friend",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loan_game_id_game",
                        column: x => x.id_game,
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loan_user_id_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friend_id_user",
                table: "friend",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_game_id_user",
                table: "game",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_loan_id_friend",
                table: "loan",
                column: "id_friend");

            migrationBuilder.CreateIndex(
                name: "IX_loan_id_game",
                table: "loan",
                column: "id_game");

            migrationBuilder.CreateIndex(
                name: "IX_loan_id_user",
                table: "loan",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loan");

            migrationBuilder.DropTable(
                name: "friend");

            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
