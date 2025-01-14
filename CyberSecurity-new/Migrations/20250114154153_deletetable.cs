using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class deletetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_RoleMaster_RoleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMaster",
                table: "RoleMaster");

            migrationBuilder.RenameTable(
                name: "RoleMaster",
                newName: "Rolemaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rolemaster",
                table: "Rolemaster",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Rolemaster_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "Rolemaster",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Rolemaster_RoleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rolemaster",
                table: "Rolemaster");

            migrationBuilder.RenameTable(
                name: "Rolemaster",
                newName: "RoleMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMaster",
                table: "RoleMaster",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_RoleMaster_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "RoleMaster",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
