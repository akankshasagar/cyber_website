using System.ComponentModel.DataAnnotations;

namespace CyberSecurity_new.Models
{
    public class CourseCompleted
    {
        [Key]
        public int Id { get; set; }
        public string email { get; set; }
        public string CourseName { get; set; }
    }
}
