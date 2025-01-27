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

        //[HttpPost]
        //public async Task<IActionResult> Enroll(string course)
        //{

        //    string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

        //    // Validate input
        //    if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(course))
        //    {
        //        return BadRequest("Email and course must be provided.");
        //    }

        //    bool isEnrolled = await _authContext.CourseEnrollments
        //                            .AnyAsync(enrollment => enrollment.Email == userEmail && enrollment.Course == course);

        //    if (isEnrolled)
        //    {
        //        return BadRequest("You are already enrolled in this course.");
        //    }

        //    var enrollment = new CourseEnrollment
        //    {
        //        Email = userEmail,
        //        Course = course
        //    };

        //    _authContext.CourseEnrollments.Add(enrollment);
        //    await _authContext.SaveChangesAsync();

        //    return Ok("Enrollment successful.");

        //}

        [HttpPost("Enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentRequest request)
        {
            // Validate request
            if (request == null || request.UserId <= 0 || request.CourseId <= 0)
            {
                return BadRequest("Invalid enrollment request.");
            }

            // Check if the user already enrolled in the course
            var existingEnrollment = _authContext.CourseEnrollment
                .FirstOrDefault(e => e.UserID == request.UserId && e.CourseId == request.CourseId);

            if (existingEnrollment != null)
            {
                return Conflict("User is already enrolled in this course.");
            }

            // Validate User and Course
            var user = await _authContext.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var course = await _authContext.course.FindAsync(request.CourseId);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

            // Create enrollment
            var enrollment = new CourseEnrollment
            {
                UserID = request.UserId,
                Email = user.Email,
                CourseId = request.CourseId,
                EnrolledAt = DateTime.Now
            };

            _authContext.CourseEnrollment.Add(enrollment);
            await _authContext.SaveChangesAsync();

            return Ok(new { Message = "User successfully enrolled in the course.", EnrollmentId = enrollment.Id });
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

    }

    public class EnrollmentRequest
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
