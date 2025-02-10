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
    public class TopicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TopicController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Modules/{courseId}/Topics
        [HttpGet("{courseId}/Topics")]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByCourse(int courseId)
        {
            var topics = await _context.Topics
                .Where(t => t.CourseId == courseId)
                .Include(t => t.Courses)  // To include course details
                .ToListAsync();

            if (topics == null || !topics.Any())
            {
                return NotFound($"No topics found for the course with ID {courseId}.");
            }

            return Ok(topics);
        }        


        [HttpGet("{courseId}/{moduleId}/Topics")]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByCourseAndModule(int courseId, int moduleId)
        {
            var topics = await _context.Topics
                .Where(t => t.CourseId == courseId && t.ModuleId == moduleId)
                .Include(t => t.Courses) // To include course details if needed
                .Include(t => t.Module)  // To include module details if needed
                .ToListAsync();

            if (topics == null || !topics.Any())
            {
                return NotFound($"No topics found for the course with ID {courseId} and module with ID {moduleId}.");
            }

            // Get the base URL (e.g., https://localhost:7243)
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            // Normalize the image path for all topics
            foreach (var topic in topics)
            {
                if (!string.IsNullOrEmpty(topic.T_ImagePath))
                {
                    // Replace backslashes with forward slashes and prepend the base URL, 
                    // ensuring we are only appending the path after `wwwroot/images/`
                    topic.T_ImagePath = $"{baseUrl}/images/topics/{topic.T_ImagePath.Substring(topic.T_ImagePath.LastIndexOf("\\") + 1).Replace("\\", "/")}";
                }
            }

            return Ok(topics);
        }        

        [HttpGet("{id}")]
        public IActionResult GetTopicById(int id)
        {
            var topic = _context.Topics
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    t.Id,
                    t.Topic_Name,
                    t.Topic_Description,
                    ImagePath = t.T_ImagePath != null
                        ? $"{Request.Scheme}://{Request.Host}/images/topics/{System.IO.Path.GetFileName(t.T_ImagePath)}"
                        : null
                })
                .FirstOrDefault();            

            return Ok(topic);
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

        [HttpPost("AddTopicToModule")]
        public async Task<IActionResult> AddTopicToModule([FromBody] List<AddTopicDto> requests)
        {
            // Get the logged-in user's email from the claims
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            // Fetch user details from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to add topics.");
            }

            if (requests == null || !requests.Any())
            {
                return BadRequest(new { message = "At least one topic is required." });
            }

            var addedTopics = new List<object>();

            foreach (var request in requests)
            {
                if (request.ModuleId <= 0 || request.CourseId <= 0 || string.IsNullOrEmpty(request.TopicName) || string.IsNullOrEmpty(request.TopicDescription))
                {
                    return BadRequest(new { message = "Module ID, Course ID, topic name, and description are required." });
                }

                var module = await _context.modules.FirstOrDefaultAsync(m => m.Id == request.ModuleId && m.CourseId == request.CourseId);

                if (module == null)
                {
                    return NotFound(new { message = $"Module with ID {request.ModuleId} in Course {request.CourseId} not found." });
                }

                string? topicImagePath = null;
                if (!string.IsNullOrEmpty(request.TImagePath))
                {
                    topicImagePath = SaveBase64Image(request.TImagePath, "wwwroot/images/topics", $"{Guid.NewGuid()}.png");
                }

                var topic = new Topic
                {
                    Topic_Name = request.TopicName,
                    Topic_Description = request.TopicDescription,
                    T_ImagePath = topicImagePath,
                    ModuleId = request.ModuleId,
                    CourseId = request.CourseId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userEmail
                };

                _context.Topics.Add(topic);
                await _context.SaveChangesAsync();

                addedTopics.Add(new { topicId = topic.Id, topicName = topic.Topic_Name });
            }

            return Ok(new { message = "Topics added successfully.", topics = addedTopics });
        }

        [HttpGet("{courseId}/{moduleId}")]
        public async Task<IActionResult> GetTopicsByModule(int courseId, int moduleId)
        {
            // Get the logged-in user's email from the claims
            //var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            //if (string.IsNullOrEmpty(userEmail))
            //{
            //    return Unauthorized(new { message = "User not authenticated." });
            //}

            // Fetch user details from the database
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            //if (user == null)
            //{
            //    return Unauthorized(new { message = "User not found." });
            //}

            //if (user.RoleId == 3)
            //{
            //    return Forbid("You do not have permission to view topics.");
            //}

            var topics = await _context.Topics
                .Where(t => t.CourseId == courseId && t.ModuleId == moduleId)
                .Select(t => new
                {
                    t.Id,
                    t.Topic_Name,
                    t.Topic_Description,
                    t.T_ImagePath,
                    t.CreatedAt
                })
                .ToListAsync();

            if (topics == null || topics.Count == 0)
            {
                return NotFound(new { message = "No topics found for the specified module and course." });
            }

            return Ok(topics);
        }

        [HttpDelete("{courseId}/{moduleId}/{topicId}")]
        public async Task<IActionResult> DeleteTopic(int courseId, int moduleId, int topicId)
        {
            // Get the logged-in user's email from the claims
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            // Fetch user details from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to delete topic.");
            }
            // Find the topic matching the given IDs
            var topic = await _context.Topics
                .FirstOrDefaultAsync(t => t.Id == topicId && t.ModuleId == moduleId && t.CourseId == courseId);

            // Check if the topic exists
            if (topic == null)
            {
                return NotFound(new { message = "Topic not found for the given course and module." });
            }

            // Remove the topic
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Topic deleted successfully." });
        }

        [HttpPut("{courseId}/{moduleId}/{topicId}")]
        public async Task<IActionResult> EditTopic(int courseId, int moduleId, int topicId, [FromBody] EditTopicDto updatedTopic)
        {
            // Get the logged-in user's email from the claims
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            // Fetch user details from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to edit topics.");
            }

            // Find the topic matching the given IDs
            var topic = await _context.Topics
                .FirstOrDefaultAsync(t => t.Id == topicId && t.ModuleId == moduleId && t.CourseId == courseId);

            if (topic == null)
            {
                return NotFound(new { message = "Topic not found for the given course and module." });
            }

            // Update only the provided values
            if (!string.IsNullOrEmpty(updatedTopic.TopicName))
            {
                topic.Topic_Name = updatedTopic.TopicName;
            }

            if (!string.IsNullOrEmpty(updatedTopic.TopicDescription))
            {
                topic.Topic_Description = updatedTopic.TopicDescription;
            }

            // If a new image is provided, update it
            if (!string.IsNullOrEmpty(updatedTopic.TImagePath))
            {
                topic.T_ImagePath = SaveBase64Image(updatedTopic.TImagePath, "wwwroot/images/topics", $"{Guid.NewGuid()}.png");
            }

            // Update timestamps and user
            topic.UpdatedAt = DateTime.Now;
            topic.UpdatedBy = userEmail;

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(new { message = "Topic updated successfully.", topic });
        }



    }

    public class AddTopicDto
    {
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string? TImagePath { get; set; } // Optional Base64 image string
    }

    public class EditTopicDto
    {
        public string? TopicName { get; set; }  // Optional, only update if provided
        public string? TopicDescription { get; set; }  // Optional, only update if provided
        public string? TImagePath { get; set; } // Optional, Base64 string for the image
    }


}
