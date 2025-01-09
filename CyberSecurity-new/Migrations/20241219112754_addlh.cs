using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class addlh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginHistory",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginExpiresIn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistory", x => x.LoginId);
                    table.ForeignKey(
                        name: "FK_LoginHistory_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistory_UserId",
                table: "LoginHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginHistory");
        }
    }
}
