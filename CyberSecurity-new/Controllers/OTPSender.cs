using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Text;
using CyberSecurity_new.Helper;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPSender : ControllerBase
    {
        private readonly SmtpClient _smtpClient;
        private readonly Random _random;
        private readonly Dictionary<string, string> _recentOtpDictionary;
        private readonly AppDbContext _context;

        public OTPSender(AppDbContext context)
        {
            _context = context;
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("it.developer@hrjohnsonindia.com", "zato qfno kekb gfbn"),
                EnableSsl = true,
            };
            _random = new Random();
            _recentOtpDictionary = new Dictionary<string, string>();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendOTP(string email)
        {
            try
            {
                string otp = GenerateOTP();
                _recentOtpDictionary[email] = otp;

                _context.OTPVerifications.Add(new OTPVerification { Email = email, OTP = otp, Status = "Pending" });
                await _context.SaveChangesAsync();
                await SendEmailAsync(email, otp);

                return Ok("OTP sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send OTP: {ex.Message}");
            }
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP(string email, string otp)
        {
            try
            {
                email = email.Trim();
                otp = otp.Trim();

                var otpVerification = _context.OTPVerifications
                        .FirstOrDefault(o => o.Email == email && o.OTP == otp && o.Status == "Pending");

                if (otpVerification != null)
                {
                    // Update the status to "Verified"
                    otpVerification.Status = "Verified";
                    _context.SaveChanges();
                    var user = _context.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        // Update the password to null
                        //user.Password = null;
                        _context.SaveChanges();
                    }

                    return Ok(new { Message = "OTP verified successfully!" });
                }
                else
                {
                    // Incorrect OTP or OTP already verified
                    return BadRequest(new { Message = "Invalid OTP!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to verify OTP: {ex.Message}");
            }
        }

        [HttpPost("reset")]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            try
            {
                email = email.Trim();
                newPassword = newPassword.Trim();

                // Find the user by email
                var user = _context.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    // Update the password to the new password
                    var pass = CheckPasswordStrength(newPassword);
                    if (!string.IsNullOrEmpty(pass))
                        return BadRequest(new { Message = pass.ToString() });
                    user.Password = PasswordHasher.HashPassword(newPassword);
                    //user.Password = newPassword;
                    _context.SaveChanges();

                    return Ok(new { Message = "Password updated successfully!" });
                }
                else
                {
                    // User not found
                    return NotFound(new { Message = "Email not found!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to reset password: {ex.Message}");
            }
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8\n" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric\n" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),-,_,+,=,`,~,\\[,\\],{,},.,/,|,;,:,?,']"))
                sb.Append("Password should contain special characters\n" + Environment.NewLine);

            return sb.ToString();
        }

        private string GenerateOTP()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            StringBuilder otp = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                otp.Append(chars[_random.Next(chars.Length)]);
            }
            return otp.ToString();
        }

        private async Task SendEmailAsync(string email, string otp)
        {
            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("it.developer@hrjohnsonindia.com"),
                    Subject = "OTP Verification",
                    Body = $"Your OTP for CyberSecurity E-learning Website is: {otp}",
                    IsBodyHtml = false
                };
                mailMessage.To.Add(email);
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }
    }
}
