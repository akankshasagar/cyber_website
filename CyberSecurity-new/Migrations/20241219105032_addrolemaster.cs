using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class addrolemaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.RoleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleMaster");
        }
    }
}
