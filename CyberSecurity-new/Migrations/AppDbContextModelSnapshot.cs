﻿// <auto-generated />
using System;
using CyberSecurity_new.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CyberSecurity_new.Models.AttackSurfaces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("attacksurfaces", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CourseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseAdded", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.CourseCompleted", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("coursesCompleted", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.CourseEnrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("coursesEnrolled", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CourseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.CyberStalkBullyTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cyberstalkbullytest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.DataProtectTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("dataprotectiontest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeptId"), 1L, 1);

                    b.Property<string>("DeptCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeptName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeptId");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.DnsAptTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("dnsapttest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.DosDontsTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("dosanddontstest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.IRMngmntTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("irmngmnttest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.IsactTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("isactest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.LoginHistory", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"), 1L, 1);

                    b.Property<DateTime>("LoginExpiresIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginId");

                    b.HasIndex("UserId");

                    b.ToTable("LoginHistory", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Module_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Module", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.OTPVerification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("otpverification", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.PhishingSpoofingTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("phishingspoofingtest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Rolemaster", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("RoleMaster", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Test01", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("test01", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("T_ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ModuleId");

                    b.ToTable("topics");
                });

            modelBuilder.Entity("CyberSecurity_new.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passowrd_Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password_Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("login_count")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.HasIndex("RoleId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.WirelessEnvironmentTest001", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Q1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Q9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("wirelessenvironmenttest001", (string)null);
                });

            modelBuilder.Entity("CyberSecurity_new.Models.LoginHistory", b =>
                {
                    b.HasOne("CyberSecurity_new.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Module", b =>
                {
                    b.HasOne("CyberSecurity_new.Models.Courses", "Courses")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("CyberSecurity_new.Models.Topic", b =>
                {
                    b.HasOne("CyberSecurity_new.Models.Courses", "Courses")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CyberSecurity_new.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("CyberSecurity_new.Models.User", b =>
                {
                    b.HasOne("CyberSecurity_new.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CyberSecurity_new.Models.Rolemaster", "Rolemaster")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Rolemaster");
                });
#pragma warning restore 612, 618
        }
    }
}
