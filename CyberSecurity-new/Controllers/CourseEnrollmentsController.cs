using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public CourseEnrollmentsController(AppDbContext authContext)
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

            bool isEnrolled = await _authContext.CourseEnrollments
                                    .AnyAsync(enrollment => enrollment.Email == userEmail && enrollment.Course == course);

            if (isEnrolled)
            {
                return BadRequest("You are already enrolled in this course.");
            }

            var enrollment = new CourseEnrollment
            {
                Email = userEmail,
                Course = course
            };

            _authContext.CourseEnrollments.Add(enrollment);
            await _authContext.SaveChangesAsync();

            return Ok("Enrollment successful.");

        }
    }
}
