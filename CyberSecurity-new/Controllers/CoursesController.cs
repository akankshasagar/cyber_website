using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CoursesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCourse([FromForm] CourseDto courseDto)
        {
            var imagePath = Path.Combine("Uploads", "Images", Guid.NewGuid() + Path.GetExtension(courseDto.Image.FileName));
            var filePath = Path.Combine("Uploads", "Files", Guid.NewGuid() + Path.GetExtension(courseDto.File.FileName));

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await courseDto.Image.CopyToAsync(stream);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await courseDto.File.CopyToAsync(stream);
            }

            var course = new Course
            {
                CourseName = courseDto.Name,
                CourseDescription = courseDto.Description,
                ImagePath = imagePath,
                FilePath = filePath
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }

    }

    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile File { get; set; }
    }
}
