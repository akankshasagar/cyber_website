using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DnsAptAnswer001 : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public DnsAptAnswer001(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody] DnsAptTest001 submission)
        {
            int marks = CalculateMarks(submission);

            if (submission == null)
            {
                return BadRequest("Invalid form submission data");
            }

            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found in the request");
            }

            var existingSubmission = await _authContext.DnsAptTest001s.FirstOrDefaultAsync(s => s.Email == userEmail);
            if (existingSubmission != null)
            {
                return BadRequest(new { Message = $"You Already submitted the form You Scored: {existingSubmission.Marks} out of 10" });
            }


            // Set the user's email to the submission object
            submission.Email = userEmail;
            submission.Marks = marks;


            // Save the submission to the database
            _authContext.DnsAptTest001s.Add(submission);
            await _authContext.SaveChangesAsync();
            return Ok(new { Message = $"You Scored: {marks} out of 10" });
        }

        private int CalculateMarks(DnsAptTest001 submission)
        {
            int marks = 0;

            Dictionary<string, string> correctAnswers = new Dictionary<string, string>
            {
                { "Q1", "e" },
                { "Q2", "d" },
                { "Q3", "c" },
                { "Q4", "b" },
                { "Q5", "e" },
                { "Q6", "c" },
                { "Q7", "b" },
                { "Q8", "a" },
                { "Q9", "b" },
                { "Q10", "a" }
            };

            if (submission.Q1 == correctAnswers["Q1"])
            {
                marks++;
            }

            if (submission.Q2 == correctAnswers["Q2"])
            {
                marks++;
            }

            if (submission.Q3 == correctAnswers["Q3"])
            {
                marks++;
            }

            if (submission.Q4 == correctAnswers["Q4"])
            {
                marks++;
            }

            if (submission.Q5 == correctAnswers["Q5"])
            {
                marks++;
            }

            if (submission.Q6 == correctAnswers["Q6"])
            {
                marks++;
            }

            if (submission.Q7 == correctAnswers["Q7"])
            {
                marks++;
            }

            if (submission.Q8 == correctAnswers["Q8"])
            {
                marks++;
            }

            if (submission.Q9 == correctAnswers["Q9"])
            {
                marks++;
            }

            if (submission.Q10 == correctAnswers["Q10"])
            {
                marks++;
            }


            return marks;


        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DnsAptTest001>> GetAllResponses()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var userResponses = await _authContext.DnsAptTest001s
                                         .Where(response => response.Email == userEmail)
                                         .ToListAsync();

            return Ok(userResponses);
        }

        /*[HttpGet("test01")]
        [Authorize]*/
        [HttpPost("dnsapttest001")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckEmailExists()
        {

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var emailExists = await _authContext.DnsAptTest001s.AnyAsync(e => e.Email == userEmail);
            if (!emailExists)
            {
                return BadRequest(new { Message = "Please Submit The Test" });
            }
            return Ok(new { Message = "You have given the test" });
        }
    }
}
