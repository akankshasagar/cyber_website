using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class deleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attacksurfaces");

            migrationBuilder.DropTable(
                name: "CourseAdded");

            migrationBuilder.DropTable(
                name: "cyberstalkbullytest001");

            migrationBuilder.DropTable(
                name: "dataprotectiontest001");

            migrationBuilder.DropTable(
                name: "dnsapttest001");

            migrationBuilder.DropTable(
                name: "dosanddontstest001");

            migrationBuilder.DropTable(
                name: "irmngmnttest001");

            migrationBuilder.DropTable(
                name: "isactest001");

            migrationBuilder.DropTable(
                name: "phishingspoofingtest001");

            migrationBuilder.DropTable(
                name: "test01");

            migrationBuilder.DropTable(
                name: "wirelessenvironmenttest001");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attacksurfaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attacksurfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseAdded",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAdded", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cyberstalkbullytest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cyberstalkbullytest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dataprotectiontest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dataprotectiontest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dnsapttest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dnsapttest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dosanddontstest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dosanddontstest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "irmngmnttest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_irmngmnttest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "isactest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isactest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phishingspoofingtest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phishingspoofingtest001", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "test01",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "wirelessenvironmenttest001",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wirelessenvironmenttest001", x => x.Id);
                });
        }
    }
}
