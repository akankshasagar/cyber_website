using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurity_new.Models
{
    public class CourseEnrollment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public string Email { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public DateTime EnrolledAt { get; set; }


        public virtual Courses Courses { get; set; }
        public virtual User User { get; set; }
    }
}
