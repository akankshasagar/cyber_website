using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var topics = await _context.topics
                .Where(t => t.CourseId == courseId)
                .Include(t => t.Courses)  // To include course details
                .ToListAsync();

            if (topics == null || !topics.Any())
            {
                return NotFound($"No topics found for the course with ID {courseId}.");
            }

            return Ok(topics);
        }

        // GET: api/Modules/{courseId}/{moduleId}/Topics
        //[HttpGet("{courseId}/{moduleId}/Topics")]
        //public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByCourseAndModule(int courseId, int moduleId)
        //{
        //    var topics = await _context.topics
        //        .Where(t => t.CourseId == courseId && t.ModuleId == moduleId)
        //        .Include(t => t.Courses) // To include course details if needed
        //        .Include(t => t.Module)  // To include module details if needed
        //        .ToListAsync();

        //    if (topics == null || !topics.Any())
        //    {
        //        return NotFound($"No topics found for the course with ID {courseId} and module with ID {moduleId}.");
        //    }

        //    return Ok(topics);
        //}



        [HttpGet("{courseId}/{moduleId}/Topics")]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByCourseAndModule(int courseId, int moduleId)
        {
            var topics = await _context.topics
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







        //[HttpGet("{id}")]
        //public async Task<ActionResult<Topic>> GetTopicById(int id)
        //{
        //    var topic = await _context.topics
        //        .Include(t => t.Courses) // Include related course details
        //        .Include(t => t.Module)  // Include related module details
        //        .FirstOrDefaultAsync(t => t.Id == id);

        //    if (topic == null)
        //    {
        //        return NotFound($"No topic found with ID {id}.");
        //    }

        //    // Append the correct path for the image
        //    topic.T_ImagePath = topic.T_ImagePath.Replace("wwwroot", "").Replace("\\", "/");

        //    return Ok(topic);
        //}

        [HttpGet("{id}")]
        public IActionResult GetTopicById(int id)
        {
            var topic = _context.topics
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

            if (topic == null)
            {
                return NotFound(new { message = "Topic not found" });
            }

            return Ok(topic);
        }



        //[HttpPost]
        //public async Task<IActionResult> AddTopic([FromBody] TopicDto topicDto)
        //{
        //    var topic = new Topic
        //    {
        //        Name = topicDto.Name,
        //        ModuleId = topicDto.ModuleId
        //    };
        //    _context.topics.Add(topic);
        //    await _context.SaveChangesAsync();
        //    return Ok(topic);
        //}

        //[HttpGet("{moduleId}")]
        //public async Task<IActionResult> GetTopics(int moduleId)
        //{
        //    var topics = await _context.topics.Where(t => t.ModuleId == moduleId).Include(t => t.Images).ToListAsync();
        //    return Ok(topics);
        //}
    }

    //public class TopicDto
    //{
    //    public string Name { get; set; }
    //    public int ModuleId { get; set; }
    //}
}
