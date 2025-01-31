using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    public partial class updatednames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Course_CourseId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Module_ModuleId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollment_Course_CourseId",
                table: "CourseEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollment_users_UserID",
                table: "CourseEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginHistory_users_UserId",
                table: "LoginHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Module_ModuleId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_topics_Course_CourseId",
                table: "topics");

            migrationBuilder.DropForeignKey(
                name: "FK_topics_Module_ModuleId",
                table: "topics");

            migrationBuilder.DropForeignKey(
                name: "FK_users_Department_DeptId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_RoleMaster_RoleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topics",
                table: "topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_otpverification",
                table: "otpverification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_coursesCompleted",
                table: "coursesCompleted");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Module",
                table: "Module");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "topics",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "otpverification",
                newName: "OtpVerification");

            migrationBuilder.RenameTable(
                name: "coursesCompleted",
                newName: "CoursesCompleted");

            migrationBuilder.RenameTable(
                name: "Module",
                newName: "Modules");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_users_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_users_DeptId",
                table: "Users",
                newName: "IX_Users_DeptId");

            migrationBuilder.RenameIndex(
                name: "IX_topics_ModuleId",
                table: "Topics",
                newName: "IX_Topics_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_topics_CourseId",
                table: "Topics",
                newName: "IX_Topics_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Module_CourseId",
                table: "Modules",
                newName: "IX_Modules_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_ModuleId",
                table: "Answers",
                newName: "IX_Answers_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_CourseId",
                table: "Answers",
                newName: "IX_Answers_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtpVerification",
                table: "OtpVerification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesCompleted",
                table: "CoursesCompleted",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Courses_CourseId",
                table: "Answers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Modules_ModuleId",
                table: "Answers",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollment_Courses_CourseId",
                table: "CourseEnrollment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollment_Users_UserID",
                table: "CourseEnrollment",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginHistory_Users_UserId",
                table: "LoginHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseId",
                table: "Modules",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Modules_ModuleId",
                table: "Question",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Modules_ModuleId",
                table: "Topics",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DeptId",
                table: "Users",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RoleMaster_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "RoleMaster",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Courses_CourseId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Modules_ModuleId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollment_Courses_CourseId",
                table: "CourseEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollment_Users_UserID",
                table: "CourseEnrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginHistory_Users_UserId",
                table: "LoginHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Modules_ModuleId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Modules_ModuleId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DeptId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RoleMaster_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtpVerification",
                table: "OtpVerification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesCompleted",
                table: "CoursesCompleted");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "topics");

            migrationBuilder.RenameTable(
                name: "OtpVerification",
                newName: "otpverification");

            migrationBuilder.RenameTable(
                name: "CoursesCompleted",
                newName: "coursesCompleted");

            migrationBuilder.RenameTable(
                name: "Modules",
                newName: "Module");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "users",
                newName: "IX_users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DeptId",
                table: "users",
                newName: "IX_users_DeptId");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_ModuleId",
                table: "topics",
                newName: "IX_topics_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_CourseId",
                table: "topics",
                newName: "IX_topics_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_CourseId",
                table: "Module",
                newName: "IX_Module_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_ModuleId",
                table: "Answer",
                newName: "IX_Answer_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_CourseId",
                table: "Answer",
                newName: "IX_Answer_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_topics",
                table: "topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_otpverification",
                table: "otpverification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coursesCompleted",
                table: "coursesCompleted",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Module",
                table: "Module",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Course_CourseId",
                table: "Answer",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Module_ModuleId",
                table: "Answer",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollment_Course_CourseId",
                table: "CourseEnrollment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollment_users_UserID",
                table: "CourseEnrollment",
                column: "UserID",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginHistory_users_UserId",
                table: "LoginHistory",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Module_ModuleId",
                table: "Question",
                column: "ModuleId",
                principalTable: "Module",
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
    }
}
