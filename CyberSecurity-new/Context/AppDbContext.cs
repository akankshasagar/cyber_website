using CyberSecurity_new.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberSecurity_new.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }        
        public DbSet<Rolemaster> rolemasters { get; set; }
        public DbSet<LoginHistory> loginhistory { get; set; }        
        public DbSet<Courses> course { get; set; }
        public DbSet<Module> modules { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollment { get; set; }
        public DbSet<CourseCompleted> courseCompleteds { get; set; }
        public DbSet<OTPVerification> OTPVerifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");                     

            modelBuilder.Entity<Department>().ToTable("Departments");                        

            modelBuilder.Entity<Rolemaster>().ToTable("RoleMaster");

            modelBuilder.Entity<LoginHistory>().ToTable("LoginHistory");

            modelBuilder.Entity<Courses>().ToTable("Courses");
            modelBuilder.Entity<Module>().ToTable("Modules");
            modelBuilder.Entity<Topic>()
            .HasOne(t => t.Module)
            .WithMany()
            .HasForeignKey(t => t.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete for Module

            modelBuilder.Entity<Topic>()
            .HasOne(t => t.Courses)
            .WithMany()
            .HasForeignKey(t => t.CourseId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete for Courses

            modelBuilder.Entity<Question>().ToTable("Question");
            // Answer -> Question
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer -> Module
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Module)
                .WithMany()
                .HasForeignKey(a => a.ModuleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer -> Courses
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Courses)
                .WithMany()
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(c => c.Courses)
                .WithMany()
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseCompleted>().ToTable("CoursesCompleted");


            //OTPVerification
            modelBuilder.Entity<OTPVerification>().ToTable("OtpVerification");
        }
    }
}
