using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class updatedtest01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Marks",
                table: "test01",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marks",
                table: "test01");
        }
    }
}
