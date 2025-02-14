using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("SubmitAnswers")]
        public async Task<IActionResult> SubmitAnswers([FromBody] List<Answer> answers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data." });
            }

            try
            {
                foreach (var answer in answers)
                {
                    // Validate QuestionId and retrieve question details
                    var question = await _context.questions
                        .Include(q => q.Module) // Assuming your question has a related module
                        .FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

                    if (question == null)
                    {
                        return NotFound(new { message = $"Invalid QuestionId {answer.QuestionId}." });
                    }

                    // Automatically get the ModuleId and CourseId from the question
                    answer.ModuleId = question.ModuleId;
                    var courseId = question.Module.CourseId;  // Assuming your module has a related course

                    // Check if the user has already submitted answers for the same ModuleId and CourseId
                    var existingAnswer = await _context.Answers
                        .FirstOrDefaultAsync(a => a.ModuleId == answer.ModuleId && a.CourseId == courseId && a.SubmittedBy == answer.SubmittedBy);

                    if (existingAnswer != null)
                    {
                        return BadRequest(new { message = "You have already submitted answers for this module." });
                    }

                    // Validate ModuleId if required
                    var moduleExists = await _context.modules.AnyAsync(m => m.Id == answer.ModuleId);
                    if (!moduleExists)
                    {
                        return NotFound(new { message = $"Invalid ModuleId {answer.ModuleId}." });
                    }

                    // Check if the answer is correct
                    answer.IsCorrect = string.Equals(answer.AnswerText?.Trim(), question.CorrectOption?.Trim(), StringComparison.OrdinalIgnoreCase);

                    // Set additional fields
                    answer.SubmittedAt = DateTime.Now;

                    // Save the answer
                    _context.Answers.Add(answer);
                }

                await _context.SaveChangesAsync();

                return Ok(new { message = "Answers submitted successfully." });
            }
            catch (Exception ex)
            {
                // Log the error and return a detailed error message
                return StatusCode(500, new { message = "An error occurred while submitting the answers.", details = ex.Message });
            }
        }


        [HttpGet("GetSubmittedAnswers/{userId}/{moduleId}")]
        public async Task<IActionResult> GetSubmittedAnswers(string userId, int moduleId)
        {
            try
            {
                var submittedAnswers = await _context.Answers
                    .Where(a => a.SubmittedBy == userId && a.ModuleId == moduleId)
                    .Include(a => a.Question) // Assuming you want question details as well
                    .ToListAsync();

                if (!submittedAnswers.Any())
                {
                    return NotFound(new { message = "No answers found for this module." });
                }

                return Ok(submittedAnswers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching answers.", details = ex.Message });
            }
        }



    }
}
