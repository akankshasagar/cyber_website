using System.ComponentModel.DataAnnotations;

namespace CyberSecurity_new.Models
{
    public class Rolemaster
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}
