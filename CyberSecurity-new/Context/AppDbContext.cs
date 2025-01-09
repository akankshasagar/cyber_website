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
        public DbSet<Test01> Test01 { get; set; }
        public DbSet<IsactTest001> IsactTest001s { get; set; }
        public DbSet<AttackSurfaces> AttackSurfacesTest { get; set; }
        public DbSet<PhishingSpoofingTest001> PhishingSpoofingTest001s { get; set; }
        public DbSet<WirelessEnvironmentTest001> WirelessEnvironmentTest001s { get; set; }
        public DbSet<DosDontsTest001> DosDontsTest001s { get; set; }
        public DbSet<IRMngmntTest001> iRMngmntTest001s { get; set; }
        public DbSet<DataProtectTest001> DataProtectTest001s { get; set; }
        public DbSet<DnsAptTest001> DnsAptTest001s { get; set; }
        public DbSet<CyberStalkBullyTest001> CyberStalkBullyTest001s { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<CourseCompleted> courseCompleteds { get; set; }
        public DbSet<OTPVerification> OTPVerifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");                     

            modelBuilder.Entity<Department>().ToTable("Department");                        

            modelBuilder.Entity<Rolemaster>().ToTable("RoleMaster");

            modelBuilder.Entity<LoginHistory>().ToTable("LoginHistory");

            modelBuilder.Entity<Test01>().ToTable("test01");
            modelBuilder.Entity<IsactTest001>().ToTable("isactest001");
            modelBuilder.Entity<AttackSurfaces>().ToTable("attacksurfaces");

            modelBuilder.Entity<PhishingSpoofingTest001>().ToTable("phishingspoofingtest001");

            modelBuilder.Entity<WirelessEnvironmentTest001>().ToTable("wirelessenvironmenttest001");

            modelBuilder.Entity<DosDontsTest001>().ToTable("dosanddontstest001");

            modelBuilder.Entity<IRMngmntTest001>().ToTable("irmngmnttest001");

            modelBuilder.Entity<DataProtectTest001>().ToTable("dataprotectiontest001");

            modelBuilder.Entity<DnsAptTest001>().ToTable("dnsapttest001");

            modelBuilder.Entity<CyberStalkBullyTest001>().ToTable("cyberstalkbullytest001");

            modelBuilder.Entity<Course>().ToTable("CourseAdded");
            modelBuilder.Entity<CourseEnrollment>().ToTable("coursesEnrolled");
            modelBuilder.Entity<CourseCompleted>().ToTable("coursesCompleted");


            //OTPVerification
            modelBuilder.Entity<OTPVerification>().ToTable("otpverification");
        }
    }
}
