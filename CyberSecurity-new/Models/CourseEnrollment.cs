using System.ComponentModel.DataAnnotations;

namespace CyberSecurity_new.Models
{
    public class CourseEnrollment
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
    }
}
