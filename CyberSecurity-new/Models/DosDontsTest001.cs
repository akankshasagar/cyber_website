using System.ComponentModel.DataAnnotations;

namespace CyberSecurity_new.Models
{
    public class DosDontsTest001
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Q1 { get; set; }

        public string Q2 { get; set; }

        public string Q3 { get; set; }

        public string Q4 { get; set; }

        public string Q5 { get; set; }
        public string Q6 { get; set; }
        public string Q7 { get; set; }
        public string Q8 { get; set; }
        public string Q9 { get; set; }
        public string Q10 { get; set; }
        public int Marks { get; internal set; }
    }
}
