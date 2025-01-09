using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurity_new.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("Rolemaster")]
        public int RoleId { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public int login_count { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Password_Hash { get; set; }
        public string Passowrd_Salt { get; set; }

        public virtual Rolemaster Rolemaster { get; set; }
        public virtual Department Department { get; set; }
    }
}
