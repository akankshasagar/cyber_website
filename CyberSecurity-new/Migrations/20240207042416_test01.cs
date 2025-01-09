using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class test01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "test01",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test01", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test01");
        }
    }
}
