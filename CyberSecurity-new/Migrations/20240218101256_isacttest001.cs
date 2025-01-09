using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class isacttest001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "isactest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isactest001", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "isactest001");
        }
    }
}
