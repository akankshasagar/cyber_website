using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class newmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Course_CoursesId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Module_CoursesId",
                table: "Module");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Module");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "topics");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Module",
                newName: "Module_Name");

            migrationBuilder.RenameColumn(
                name: "ThumbnailImage",
                table: "Course",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Course",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Course",
                newName: "CourseDescription");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "topics",
                newName: "Topic_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_ModuleId",
                table: "topics",
                newName: "IX_topics_ModuleId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Module",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Module",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "topics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "topics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "topics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "T_ImagePath",
                table: "topics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic_Description",
                table: "topics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_topics",
                table: "topics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Module_CourseId",
                table: "Module",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_topics_CourseId",
                table: "topics",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_topics_Course_CourseId",
                table: "topics",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_topics_Module_ModuleId",
                table: "topics",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_topics_Course_CourseId",
                table: "topics");

            migrationBuilder.DropForeignKey(
                name: "FK_topics_Module_ModuleId",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "IX_Module_CourseId",
                table: "Module");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topics",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "IX_topics_CourseId",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "T_ImagePath",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "Topic_Description",
                table: "topics");

            migrationBuilder.RenameTable(
                name: "topics",
                newName: "Topic");

            migrationBuilder.RenameColumn(
                name: "Module_Name",
                table: "Module",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Course",
                newName: "ThumbnailImage");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Course",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CourseDescription",
                table: "Course",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Topic_Name",
                table: "Topic",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_topics_ModuleId",
                table: "Topic",
                newName: "IX_Topic_ModuleId");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Module",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Topic",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Module_CoursesId",
                table: "Module",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_TopicId",
                table: "Image",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Course_CoursesId",
                table: "Module",
                column: "CoursesId",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id");
        }
    }
}
