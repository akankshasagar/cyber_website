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
