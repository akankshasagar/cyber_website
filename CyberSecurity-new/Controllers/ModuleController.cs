using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //[HttpPost]
        //public async Task<IActionResult> AddModule([FromBody] ModuleDto moduleDto)
        //{
        //    var module = new Module
        //    {
        //        Name = moduleDto.Name,
        //        CourseId = moduleDto.CourseId
        //    };
        //    _context.modules.Add(module);
        //    await _context.SaveChangesAsync();
        //    return Ok(module);
        //}

        //[HttpGet("{courseId}")]
        //public async Task<IActionResult> GetModules(int courseId)
        //{
        //    var modules = await _context.modules.Where(m => m.CourseId == courseId).Include(m => m.Topics).ToListAsync();
        //    return Ok(modules);
        //}
    }

    //public class ModuleDto
    //{
    //    public string Name { get; set; }
    //    public int CourseId { get; set; }
    //}
}
