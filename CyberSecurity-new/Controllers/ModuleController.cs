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
