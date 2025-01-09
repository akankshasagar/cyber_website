using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurity_new.Models
{
    public class LoginHistory
    {
        [Key]
        public int LoginId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LoginExpiresIn { get; set; }

        public virtual User User { get; set; }
    }
}
