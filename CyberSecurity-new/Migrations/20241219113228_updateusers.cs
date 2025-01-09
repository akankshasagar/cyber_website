using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class updateusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Passowrd_Salt",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password_Hash",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "login_count",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_DeptId",
                table: "users",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Department_DeptId",
                table: "users",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_RoleMaster_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "RoleMaster",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Department_DeptId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_RoleMaster_RoleId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_DeptId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_RoleId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Passowrd_Salt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Password_Hash",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "login_count",
                table: "users");
        }
    }
}
