using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Topic_TopicId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Module_CourseAdded_CourseId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Module_CourseId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Image",
                newName: "ImageData");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Topic",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Topic_TopicId",
                table: "Image",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Topic_TopicId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic");

            migrationBuilder.RenameColumn(
                name: "ImageData",
                table: "Image",
                newName: "Data");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Topic",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Module",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Module_CourseId",
                table: "Module",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Topic_TopicId",
                table: "Image",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Module_CourseAdded_CourseId",
                table: "Module",
                column: "CourseId",
                principalTable: "CourseAdded",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
