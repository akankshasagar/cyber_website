using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /*[HttpPost("add")]
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
        }*/

        [HttpPost("add")]
        public async Task<IActionResult> AddCourse([FromForm] CourseDto courseDto)
        {
            // Validate if required fields are null or empty
            if (string.IsNullOrWhiteSpace(courseDto.Name))
            {
                return BadRequest(new { Message = "Course name is required." });
            }

            if (string.IsNullOrWhiteSpace(courseDto.Description))
            {
                return BadRequest(new { Message = "Course description is required." });
            }

            if (courseDto.Image == null)
            {
                return BadRequest(new { Message = "Image is required." });
            }

            if (courseDto.File == null)
            {
                return BadRequest(new { Message = "File is required." });
            }

            var imageFileName = Guid.NewGuid() + Path.GetExtension(courseDto.Image.FileName);
            var fileFileName = Guid.NewGuid() + Path.GetExtension(courseDto.File.FileName);

            var imagePath = Path.Combine("Uploads", "Images", imageFileName);
            var filePath = Path.Combine("Uploads", "Files", fileFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Validate the image dimensions
            using (var imageStream = courseDto.Image.OpenReadStream())
            using (var image = System.Drawing.Image.FromStream(imageStream))
            {
                if (image.Width <= image.Height)
                {
                    return BadRequest("The image must be horizontal (width > height).");
                }
            }

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await courseDto.Image.CopyToAsync(stream);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await courseDto.File.CopyToAsync(stream);
            }

            // Get the current time in IST
            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var dateAddedIST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

            var course = new Course
            {
                CourseName = courseDto.Name,
                CourseDescription = courseDto.Description,
                ImagePath = $"/Uploads/Images/{imageFileName}",
                FilePath = $"/Uploads/Files/{fileFileName}",
                CreatedAt = dateAddedIST
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Course Added Successfully!"
            });
        }


        [HttpGet]
        public IActionResult GetCourses()
        {

            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            var courses = _context.Courses
                .Select(course => new
                {
                    course.Id,
                    course.CourseName,
                    course.CourseDescription,
                    ImagePath = $"{Request.Scheme}://{Request.Host}{course.ImagePath}",
                    FilePath = $"{Request.Scheme}://{Request.Host}{course.FilePath}",
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(course.CreatedAt, indianTimeZone)
                })
                .ToList();

            return Ok(courses);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetCourseById(int id)
        //{
        //    try
        //    {
        //        var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        //        // Fetch the course from the database
        //        var course = _context.Courses
        //            .Where(c => c.Id == id)
        //            .Select(c => new
        //            {
        //                c.Id,
        //                c.CourseName,
        //                c.CourseDescription,
        //                ImagePath = c.ImagePath != null ? $"{Request.Scheme}://{Request.Host}{c.ImagePath}" : null,
        //                FilePath = c.FilePath != null ? $"{Request.Scheme}://{Request.Host}{c.FilePath}" : null,                        
        //                c.CreatedAt
        //            })
        //            .FirstOrDefault();

        //        if (course == null)
        //        {
        //            return NotFound(new { message = "Course not found" });
        //        }

        //        // Convert CreatedAt to IST
        //        var convertedCreatedAt = TimeZoneInfo.ConvertTimeFromUtc(course.CreatedAt, indianTimeZone);

        //        // Read the file content if the file exists
        //        string fileContent = null;
        //        if (!string.IsNullOrEmpty(course.FilePath) && System.IO.File.Exists(course.FilePath))
        //        {
        //            fileContent = System.IO.File.ReadAllText(course.FilePath);
        //        }

        //        // Return the response with file content
        //        return Ok(new
        //        {
        //            course.Id,
        //            course.CourseName,
        //            course.CourseDescription,
        //            course.ImagePath,
        //            course.FilePath,
        //            CreatedAt = convertedCreatedAt,
        //            FileContent = fileContent // Include the file content in the response
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception if needed
        //        Console.WriteLine($"Error in GetCourseById: {ex.Message}");

        //        // Return a generic error response
        //        return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
        //    }
        //}

        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.CourseName,
                    c.CourseDescription,
                    ImagePath = c.ImagePath != null ? $"{Request.Scheme}://{Request.Host}{c.ImagePath}" : null,
                    FilePath = c.FilePath != null ? $"{Request.Scheme}://{Request.Host}{c.FilePath}" : null,
                    FileContent = c.FilePath != null && System.IO.File.Exists(c.FilePath)
                        ? System.IO.File.ReadAllText(c.FilePath)
                        : null
                })
                .FirstOrDefault();

            if (course == null)
            {
                return NotFound(new { message = "Course not found" });
            }

            return Ok(course);
        }


        [HttpGet("download/{id}")]
        public IActionResult DownloadCourseFile(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null || string.IsNullOrEmpty(course.FilePath))
            {
                return NotFound("File not found.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), course.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File does not exist.");
            }

            var fileName = Path.GetFileName(filePath);
            var contentType = "application/octet-stream";
            return PhysicalFile(filePath, contentType, fileName);
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
