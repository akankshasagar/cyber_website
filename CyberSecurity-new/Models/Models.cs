using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurity_new.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }


    public class Module
    {
        public int Id { get; set; }
        public string Module_Name { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public virtual Courses Courses { get; set; }
    }


    public class Topic
    {
        public int Id { get; set; }
        public string Topic_Name { get; set; }
        public string Topic_Description { get; set; }
        public string T_ImagePath { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public virtual Courses Courses { get; set; }
        public virtual Module Module { get; set; }

    }
}
