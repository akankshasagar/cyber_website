using System.ComponentModel.DataAnnotations;

namespace CyberSecurity_new.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
    }
}
