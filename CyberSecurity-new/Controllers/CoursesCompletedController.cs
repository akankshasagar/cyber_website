using CyberSecurity_new.Context;
using CyberSecurity_new.Migrations;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesCompletedController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public CoursesCompletedController(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(string course)
        {

            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            // Validate input
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(course))
            {
                return BadRequest("Email and course must be provided.");
            }

            bool isCompleted = await _authContext.courseCompleteds
                                    .AnyAsync(coursecompleted => coursecompleted.email == userEmail && coursecompleted.CourseName == course);

            if (isCompleted)
            {
                return Ok(new { Message = "Already Marked as Completed." });
            }

            var coursecompleted = new CourseCompleted
            {
                email = userEmail,
                CourseName = course
            };

            _authContext.courseCompleteds.Add(coursecompleted);
            await _authContext.SaveChangesAsync();

            return Ok("Successfully Completed The Course");

        }
    }
}
