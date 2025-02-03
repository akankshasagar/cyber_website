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
    public class ModuleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModuleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Modules/{courseId}
        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByCourse(int courseId)
        {
            var modules = await _context.modules
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Courses) // To include the Course details, if needed.
                .ToListAsync();

            if (modules == null || !modules.Any())
            {
                return NotFound($"No modules found for the course with ID {courseId}.");
            }

            return Ok(modules);
        }

        private string? SaveBase64Image(string? base64Image, string folderPath, string fileName)
        {
            //if (string.IsNullOrEmpty(base64Image) || !base64Image.Contains("base64"))
            //{
            //    throw new ArgumentException("Invalid base64 image string.");
            //}

            if (string.IsNullOrEmpty(base64Image))
            {
                return null; // No image to save
            }

            // Extract the directory path and ensure it exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var base64Data = base64Image.Split(',')[1]; // Extract Base64 data
            var imageData = Convert.FromBase64String(base64Data);

            var filePath = Path.Combine(folderPath, fileName);

            try
            {
                System.IO.File.WriteAllBytes(filePath, imageData);
                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                throw;
            }
        }

        [HttpPost("AddModuleToCourse")]
        public async Task<IActionResult> AddModuleToCourse([FromBody] ModuleWithTopicsDto request)
        {
            // Get the logged-in user's email from claims (JWT or session)
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            // Fetch the logged-in user's details
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            // Validate user's role (RoleId != 3 is required)
            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to add a module.");
            }

            // Validate the request
            if (request == null || string.IsNullOrEmpty(request.ModuleName))
            {
                return BadRequest(new { message = "Module name is required." });
            }

            // Ensure the course exists
            var course = await _context.course.FindAsync(request.CourseId);
            if (course == null)
            {
                return NotFound(new { message = "Course not found." });
            }

            // Validate that at least one topic is provided
            if (request.Topics == null || request.Topics.Count == 0)
            {
                return BadRequest(new { message = "At least one topic is required to add a module." });
            }

            // Validate that at least one question is provided
            if (request.Questions == null || request.Questions.Count == 0)
            {
                return BadRequest(new { message = "Each module must have at least one question." });
            }

            // Create and save the module
            var module = new Module
            {
                Module_Name = request.ModuleName,
                CourseId = request.CourseId,
                CreatedAt = DateTime.Now,
                CreatedBy = userEmail
            };

            _context.modules.Add(module);
            await _context.SaveChangesAsync();

            // Add topics to the module
            foreach (var topicDto in request.Topics)
            {
                var topicImagePath = !string.IsNullOrEmpty(topicDto.TImagePath)
                    ? SaveBase64Image(topicDto.TImagePath, "wwwroot/images/topics", $"{Guid.NewGuid()}.png")
                    : null;

                var topic = new Topic
                {
                    Topic_Name = topicDto.TopicName,
                    Topic_Description = topicDto.TopicDescription,
                    T_ImagePath = topicImagePath,
                    ModuleId = module.Id,
                    CourseId = request.CourseId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userEmail
                };

                _context.Topics.Add(topic);
            }

            // Add questions to the module
            if (request.Questions != null && request.Questions.Count > 0)
            {
                foreach (var questionDto in request.Questions)
                {
                    var question = new Question
                    {
                        QuestionText = questionDto.QuestionText,
                        OptionA = questionDto.OptionA,
                        OptionB = questionDto.OptionB,
                        OptionC = questionDto.OptionC,
                        OptionD = questionDto.OptionD,
                        CorrectOption = questionDto.CorrectOption,
                        ModuleId = module.Id,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userEmail
                    };

                    _context.questions.Add(question);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Module, topics, and questions added successfully.", moduleId = module.Id });
        }


        [HttpPut("EditModuleDetails")]
        public async Task<IActionResult> EditModuleDetails([FromBody] EditModuleDto request)
        {
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to edit a module.");
            }

            var module = await _context.modules.FindAsync(request.ModuleId);
            if (module == null)
            {
                return NotFound(new { message = "Module not found." });
            }

            module.Module_Name = request.ModuleName;
            module.UpdatedAt = DateTime.Now;
            module.UpdatedBy = userEmail;

            _context.modules.Update(module);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Module details updated successfully." });
        }


        [HttpDelete("DeleteModule/{courseId}/{moduleId}")]
        public async Task<IActionResult> DeleteModule(int courseId, int moduleId)
        {
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            // Get the logged-in user's details
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            // Validate that the user has permission to delete the module
            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to delete a module.");
            }

            // Check if the module exists and belongs to the given course
            var module = await _context.modules.FirstOrDefaultAsync(m => m.Id == moduleId && m.CourseId == courseId);
            if (module == null)
            {
                return NotFound(new { message = "Module not found for the specified course." });
            }

            // Delete related topics
            var topics = await _context.Topics.Where(t => t.ModuleId == moduleId).ToListAsync();
            if (topics.Any())
            {
                _context.Topics.RemoveRange(topics);
            }

            // Delete related questions
            var questions = await _context.questions.Where(q => q.ModuleId == moduleId).ToListAsync();
            if (questions.Any())
            {
                _context.questions.RemoveRange(questions);
            }

            // Delete related answers
            var answers = await _context.Answers.Where(a => a.ModuleId == moduleId).ToListAsync();
            if (answers.Any())
            {
                _context.Answers.RemoveRange(answers);
            }

            // Delete the module itself
            _context.modules.Remove(module);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { message = "Module and related data deleted successfully." });
        }


    }

    public class EditModuleDto
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }


    public class ModuleWithTopicsDto
    {
        public string ModuleName { get; set; }
        public int CourseId { get; set; }
        public List<TopicDt> Topics { get; set; }
        public List<QuestionDt> Questions { get; set; }
    }

    public class TopicDt
    {
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string TImagePath { get; set; } // Base64 string of the image
    }

    public class QuestionDt
    {
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
    }

}
