namespace CyberSecurity_new.Models
{
    public class OTPVerification
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public string Status { get; set; }
    }
}
