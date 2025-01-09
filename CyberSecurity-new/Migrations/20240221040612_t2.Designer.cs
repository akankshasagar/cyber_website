﻿// <auto-generated />
using CyberSecurity_new.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CyberSecurity_new.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240221040612_t2")]
    partial class t2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("CyberSecurity_new.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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
#pragma warning restore 612, 618
        }
    }
}
