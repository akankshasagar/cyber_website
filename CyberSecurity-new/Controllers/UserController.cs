using CyberSecurity_new.Context;
using CyberSecurity_new.Helper;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public UserController(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        private Task<bool> CheckEmailExistsAsync(string Email)
            => _authContext.Users.AnyAsync(x => x.Email == Email);

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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //check email
            if (await CheckEmailExistsAsync(userObj.Email))
                return BadRequest(new { Message = "Email already Exists!" });

            //check password strength
            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            // Hash the password and get the hash and salt
            var (passwordHash, passwordSalt) = PasswordHasher2.HashPassword(userObj.Password);

            userObj.RoleId = userObj.RoleId == 0 ? 3 : userObj.RoleId;

            // Handle DeptName to fetch DeptId if not provided
            if (userObj.DeptId == 0)
            {
                if (string.IsNullOrEmpty(userObj.Department?.DeptName))
                    return BadRequest(new { Message = "Either DeptId or DeptName must be provided" });

                // Fetch the DeptId based on DeptName
                var department = await _authContext.Departments
                    .FirstOrDefaultAsync(d => d.DeptName == userObj.Department.DeptName);

                if (department == null)
                    return BadRequest(new { Message = "Invalid Department Name provided" });

                // Assign the fetched DeptId
                userObj.DeptId = department.DeptId;
            }
            else
            {
                // Validate DeptId if provided
                var department = await _authContext.Departments
                    .FirstOrDefaultAsync(d => d.DeptId == userObj.DeptId);

                if (department == null)
                    return BadRequest(new { Message = "Invalid Department selected" });
            }

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            // Store the hash and salt in the database
            userObj.Password_Hash = passwordHash;
            userObj.Passowrd_Salt = passwordSalt;
            userObj.CreatedDate = DateTime.UtcNow;
            // Ensure no new Department is linked
            userObj.Department = null;
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            //check email format
            if (!IsEmailValid(userObj.Email))
            {
                return BadRequest(new { Message = "Invalid email format" });
            }

            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Email == userObj.Email);

            if (user == null )
                return BadRequest(new { Message = "User Not Found" });            

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }

            return Ok(new
            {
                Token = CreateJwtToken(user),
                Message = $"Welcome, {user.Name}"
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> DeleteColumnForEmail([FromBody] User userObj)
        {
            // Find the user by email
            var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Email == userObj.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            // Update the specific column to null or default value
            // Replace "SpecificColumn" with the name of the column you want to update
            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });
            user.Password = PasswordHasher.HashPassword(userObj.Password); // or assign to a default value

            // Save changes to the database
            try
            {
                await _authContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Failed to update user");
            }

            return Ok(new { Message = "Password Updated Successfully" });
        }

        private bool IsEmailValid(string email)
        {
            // Add your email format validation logic here
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return emailRegex.IsMatch(email);
        }

        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.RoleId}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
