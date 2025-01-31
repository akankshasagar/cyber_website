using System.Security.Claims;
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
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

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

        //[HttpPost("AddCourseWithModulesAndTopics")]
        //public async Task<IActionResult> AddCourseWithModulesAndTopics([FromBody] CourseWithModulesAndTopicsDto request)
        //{
        //    if (request == null || string.IsNullOrEmpty(request.CourseName))
        //    {
        //        return BadRequest(new { message = "Course data is required." });
        //    }

        //    // Create and save the course
        //    var course = new Courses
        //    {
        //        CourseName = request.CourseName,
        //        CourseDescription = request.CourseDescription,
        //        ImagePath = request.ImagePath,
        //        CreatedAt = DateTime.Now
        //    };

        //    _context.course.Add(course);
        //    await _context.SaveChangesAsync();

        //    // Add modules with topics
        //    foreach (var moduleDto in request.Modules)
        //    {
        //        var module = new Module
        //        {
        //            Module_Name = moduleDto.ModuleName,
        //            CourseId = course.Id,
        //            CreatedAt = DateTime.Now
        //        };

        //        _context.modules.Add(module);
        //        await _context.SaveChangesAsync();

        //        foreach (var topicDto in moduleDto.Topics)
        //        {
        //            var topic = new Topic
        //            {
        //                Topic_Name = topicDto.TopicName,
        //                Topic_Description = topicDto.TopicDescription,
        //                T_ImagePath = topicDto.TImagePath,
        //                ModuleId = module.Id,
        //                CourseId = course.Id,
        //                CreatedAt = DateTime.Now
        //            };

        //            _context.topics.Add(topic);
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Course, modules, and topics added successfully.", courseId = course.Id });
        //}        


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


        [HttpPost("AddCourseWithModulesAndTopics")]
        public async Task<IActionResult> AddCourseWithModulesAndTopics([FromBody] CourseWithModulesAndTopicsDto request)
        {

            // Get the logged-in user's email from the claims (e.g., from JWT or session)
            var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "User not authenticated." });
            }

            // Fetch the logged-in user's details from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            // Validate user's role (RoleId != 3 is required)
            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to add a course.");
            }

            // Validate that the course name and description are provided
            if (request == null || string.IsNullOrEmpty(request.CourseName))
            {
                return BadRequest(new { message = "Course name is required." });
            }

            if (string.IsNullOrEmpty(request.CourseDescription))
            {
                return BadRequest(new { message = "Course description is required." });
            }

            // Validate that at least one module is provided
            if (request.Modules == null || request.Modules.Count == 0)
            {
                return BadRequest(new { message = "At least one module is required to add a course." });
            }

            // Validate that each module has at least one topic
            //foreach (var module in request.Modules)
            //{
            //    if (module.Topics == null || module.Topics.Count == 0)
            //    {
            //        return BadRequest(new { message = "Each module must have at least one topic." });
            //    }
            //}

            foreach (var module in request.Modules)
            {
                if (module.Topics == null || module.Topics.Count == 0)
                {
                    return BadRequest(new { message = "Each module must have at least one topic." });
                }

                if (module.Questions == null || module.Questions.Count == 0)
                {
                    return BadRequest(new { message = "Each module must have Questions." });
                }
            }

            // Ensure the course image is provided
            if (string.IsNullOrEmpty(request.ImagePath))
            {
                return BadRequest(new { message = "Course image is required." });
            }

            // Save the course image
            var courseImagePath = SaveBase64Image(request.ImagePath, "wwwroot/images/courses", $"{Guid.NewGuid()}.png");

            // Create and save the course
            var course = new Courses
            {
                CourseName = request.CourseName,
                CourseDescription = request.CourseDescription,
                ImagePath = courseImagePath,
                CreatedAt = DateTime.Now,
                CreatedBy = userEmail
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
                    CreatedAt = DateTime.Now,
                    CreatedBy = userEmail
                };

                _context.modules.Add(module);
                await _context.SaveChangesAsync();

                foreach (var topicDto in moduleDto.Topics)
                {
                    // Save topic image if provided
                    var topicImagePath = !string.IsNullOrEmpty(topicDto.TImagePath)
                        ? SaveBase64Image(topicDto.TImagePath, "wwwroot/images/topics", $"{Guid.NewGuid()}.png")
                        : null;

                    var topic = new Topic
                    {
                        Topic_Name = topicDto.TopicName,
                        Topic_Description = topicDto.TopicDescription,
                        T_ImagePath = topicImagePath,
                        ModuleId = module.Id,
                        CourseId = course.Id,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userEmail
                    };

                    _context.Topics.Add(topic);
                }
                await _context.SaveChangesAsync();

                foreach (var questionDto in moduleDto.Questions)
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

            return Ok(new { message = "Course, modules, and topics added successfully.", courseId = course.Id });
        }


        [HttpGet]
        public IActionResult GetCourses()
        {

            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            var courses = _context.course
                .Select(course => new
                {
                    course.Id,
                    course.CourseName,
                    course.CourseDescription,
                    ImagePath = $"{Request.Scheme}://{Request.Host}/images/courses/{System.IO.Path.GetFileName(course.ImagePath)}",
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(course.CreatedAt, indianTimeZone)
                })
                .ToList();

            return Ok(courses);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetCourseById(int id)
        //{
        //    var course = _context.course
        //        .Where(c => c.Id == id)
        //        .Select(c => new
        //        {
        //            c.Id,
        //            c.CourseName,
        //            c.CourseDescription,
        //            ImagePath = c.ImagePath != null ? $"{Request.Scheme}://{Request.Host}/images/courses/{System.IO.Path.GetFileName(c.ImagePath)}" : null
        //        })
        //        .FirstOrDefault();

        //    if (course == null)
        //    {
        //        return NotFound(new { message = "Course not found" });
        //    }

        //    return Ok(course);
        //}


        [HttpGet("{id}/{courseName}")]
        public IActionResult GetCourseById(int id)
        {
            var course = _context.course
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.CourseName,
                    c.CourseDescription,
                    ImagePath = c.ImagePath != null ? $"{Request.Scheme}://{Request.Host}/images/courses/{System.IO.Path.GetFileName(c.ImagePath)}" : null
                })
                .FirstOrDefault();

            if (course == null)
            {
                return NotFound(new { message = "Course not found" });
            }

            return Ok(course);
        }        


        [HttpPost("AddModuleToCourse")]
        public async Task<IActionResult> AddModuleToCourse([FromBody] ModuleWithTopicsAndQuestionsDto request)
        {
            // Get the logged-in user's email from the claims (e.g., from JWT or session)
            //var userEmail = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            //if (string.IsNullOrEmpty(userEmail))
            //{
            //    return Unauthorized(new { message = "User not authenticated." });
            //}

            // Fetch the logged-in user's details from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "it.developer@hrjohnsonindia.com");

            //if (user == null)
            //{
            //    return Unauthorized(new { message = "User not found." });
            //}

            // Validate user's role (RoleId != 3 is required)
            //if (user.RoleId == 3)
            //{
            //    return Forbid("You do not have permission to add a module.");
            //}

            // Validate request
            if (request == null || request.CourseId <= 0 || string.IsNullOrEmpty(request.ModuleName))
            {
                return BadRequest(new { message = "Course ID and Module Name are required." });
            }

            // Check if the course exists
            var course = await _context.course.FirstOrDefaultAsync(c => c.Id == request.CourseId);
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
                CreatedBy = user.Email
            };

            _context.modules.Add(module);
            await _context.SaveChangesAsync();

            // Add topics to the module
            foreach (var topicDto in request.Topics)
            {
                // Save topic image if provided
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
                    CreatedBy = user.Email
                };

                _context.Topics.Add(topic);
            }
            await _context.SaveChangesAsync();

            // Add questions to the module
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
                    CreatedBy = user.Email
                };

                _context.questions.Add(question);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Module added successfully.", moduleId = module.Id });
        }

        [HttpPut("EditCourse/{courseId}")]
        public async Task<IActionResult> EditCourse(int courseId, [FromForm] EditCourseDto request, [FromForm] IFormFile imagePath)
        {
            // Get the logged-in user's email from the claims
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "it.developer@hrjohnsonindia.com");

            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            // Check if the course exists
            var course = await _context.course.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound(new { message = "Course not found." });
            }

            // Validate that the user has permission to edit the course
            if (user.RoleId == 3)
            {
                return Forbid("You do not have permission to edit this course.");
            }

            // Update course details
            if (!string.IsNullOrEmpty(request.CourseName))
            {
                course.CourseName = request.CourseName;
            }

            if (!string.IsNullOrEmpty(request.CourseDescription))
            {
                course.CourseDescription = request.CourseDescription;
            }

            // Update course image if provided
            if (imagePath != null)
            {
                var newImagePath = SaveImage(imagePath, "wwwroot/images/courses", $"{Guid.NewGuid()}.png");
                course.ImagePath = newImagePath;
            }

            _context.course.Update(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course details updated successfully.", courseId = course.Id });
        }

        private string SaveImage(IFormFile imageFile, string targetDirectory, string fileName)
        {
            // Ensure the directory exists
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), targetDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Define the full file path to save the image
            var filePath = Path.Combine(directoryPath, fileName);

            // Save the image to the file system
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            // Return the relative path to the image file with the required slash format
            var relativePath = Path.Combine(targetDirectory, fileName);  // wwwroot/images/courses/filename.png
            var finalPath = relativePath.Replace("/", "\\");  // Replaces all forward slashes with backslashes
            finalPath = finalPath.Replace("\\courses\\", "/courses\\");  // Ensures the correct format for the courses directory
            return finalPath;
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
        public List<QuestionDto> Questions { get; set; }
    }

    public class TopicDto
    {
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string TImagePath { get; set; }
    }

    public class QuestionDto
    {
        public string QuestionText { get; set; } // The actual question
        public string OptionA { get; set; }     // Option A
        public string OptionB { get; set; }     // Option B
        public string OptionC { get; set; }     // Option C
        public string OptionD { get; set; }     // Option D
        public string CorrectOption { get; set; } // Correct answer
    }

    public class ModuleWithTopicsAndQuestionsDto
    {
        public int CourseId { get; set; }
        public string ModuleName { get; set; }
        public List<TopicDto2> Topics { get; set; }
        public List<QuestionDto2> Questions { get; set; }
    }

    public class TopicDto2
    {
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string? TImagePath { get; set; }  // Optional topic image
    }

    public class QuestionDto2
    {
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
    }

    public class EditCourseDto
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ImagePath { get; set; } // Base64 encoded image
    }


}
