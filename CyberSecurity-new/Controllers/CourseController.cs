using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;    

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> AddCourse([FromForm] CoursesDto courseDto)
        //{
        //    // Validate if required fields are null or empty
        //    if (string.IsNullOrWhiteSpace(courseDto.Name))
        //    {
        //        return BadRequest(new { Message = "Course name is required." });
        //    }

        //    if (string.IsNullOrWhiteSpace(courseDto.Description))
        //    {
        //        return BadRequest(new { Message = "Course description is required." });
        //    }

        //    if (courseDto.Image == null)
        //    {
        //        return BadRequest(new { Message = "Image is required." });
        //    }

        //    if (courseDto.ModuleNames == null || !courseDto.ModuleNames.Any())
        //    {
        //        return BadRequest(new { Message = "At least one module name is required." });
        //    }

        //    var imageFileName = Guid.NewGuid() + Path.GetExtension(courseDto.Image.FileName);

        //    var imagePath = Path.Combine("Uploads", "Images", imageFileName);

        //    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

        //    // Validate the image dimensions
        //    using (var imageStream = courseDto.Image.OpenReadStream())
        //    using (var image = System.Drawing.Image.FromStream(imageStream))
        //    {
        //        if (image.Width <= image.Height)
        //        {
        //            return BadRequest("The image must be horizontal (width > height).");
        //        }
        //    }

        //    using (var stream = new FileStream(imagePath, FileMode.Create))
        //    {
        //        await courseDto.Image.CopyToAsync(stream);
        //    }


        //    // Get the current time in IST
        //    var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //    var dateAddedIST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

        //    var course = new Courses
        //    {
        //        CourseName = courseDto.Name,
        //        CourseDescription = courseDto.Description,
        //        ImagePath = $"/Uploads/Images/{imageFileName}",
        //        CreatedAt = dateAddedIST
        //    };

        //    _context.course.Add(course);
        //    await _context.SaveChangesAsync();

        //    // Add modules for the course
        //    foreach (var moduleName in courseDto.ModuleNames)
        //    {
        //        var module = new Module
        //        {
        //            Module_Name = moduleName,
        //            CourseId = course.Id, // Associate the module with the newly created course
        //            CreatedAt = dateAddedIST
        //        };

        //        _context.modules.Add(module);
        //    }

        //    // Save changes to add the modules
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        Message = "Course and modules added successfully!"
        //    });
        //}

        [HttpPost("AddCourseWithModulesAndTopics")]
        public async Task<IActionResult> AddCourseWithModulesAndTopics([FromBody] CourseWithModulesAndTopicsDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.CourseName))
            {
                return BadRequest(new { message = "Course data is required." });
            }

            // Create and save the course
            var course = new Courses
            {
                CourseName = request.CourseName,
                CourseDescription = request.CourseDescription,
                ImagePath = request.ImagePath,
                CreatedAt = DateTime.Now
            };

            _context.course.Add(course);
            await _context.SaveChangesAsync();

            // Add modules with topics
            foreach (var moduleDto in request.Modules)
            {
                var module = new Module
                {
                    Module_Name = moduleDto.ModuleName,
                    CourseId = course.Id,
                    CreatedAt = DateTime.Now
                };

                _context.modules.Add(module);
                await _context.SaveChangesAsync();

                foreach (var topicDto in moduleDto.Topics)
                {
                    var topic = new Topic
                    {
                        Topic_Name = topicDto.TopicName,
                        Topic_Description = topicDto.TopicDescription,
                        T_ImagePath = topicDto.TImagePath,
                        ModuleId = module.Id,
                        CourseId = course.Id,
                        CreatedAt = DateTime.Now
                    };

                    _context.topics.Add(topic);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Course, modules, and topics added successfully.", courseId = course.Id });
        }




    }

    //public class CoursesDto
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public IFormFile Image { get; set; }
    //    public List<string> ModuleNames { get; set; } // List of module names
    //}

    public class CourseWithModulesAndTopicsDto
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ImagePath { get; set; }
        public List<ModuleDto> Modules { get; set; }
    }

    public class ModuleDto
    {
        public string ModuleName { get; set; }
        public List<TopicDto> Topics { get; set; }
    }

    public class TopicDto
    {
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string TImagePath { get; set; }
    }


}
