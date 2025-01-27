using System.Security.Claims;
using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionsController(AppDbContext context)
        {
            _context = context;
        }
        

        // GET: api/Questions/Module/{moduleId}
        [HttpGet("Module/{moduleId}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByModuleId(int moduleId)
        {
            // Get the logged-in user's email from the claims (e.g., from JWT or session)
            //var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            //if (string.IsNullOrEmpty(userEmail))
            //{
            //    return Unauthorized(new { message = "User not authenticated." });
            //} 

            try
            {
                var questions = await _context.questions
                    .Where(q => q.ModuleId == moduleId) // Filter by ModuleId
                    .Include(q => q.Module) // Optional: Include Module details if needed
                    .ToListAsync();

                if (questions == null || questions.Count == 0)
                {
                    return NotFound(new { message = $"No questions found for Module: {moduleId}" });
                }

                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching questions.", details = ex.Message });
            }
        }

    }
}
