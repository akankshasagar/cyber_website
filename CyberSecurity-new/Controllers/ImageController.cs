using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddImage([FromForm] IFormFile imageFile, [FromForm] int topicId)
        //{
        //    if (imageFile != null && imageFile.Length > 0)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await imageFile.CopyToAsync(memoryStream);
        //            var image = new Image
        //            {
        //                Data = memoryStream.ToArray(),
        //                FileName = imageFile.FileName,
        //                TopicId = topicId
        //            };
        //            _context.images.Add(image);
        //            await _context.SaveChangesAsync();
        //            return Ok(image);
        //        }
        //    }
        //    return BadRequest("No image file provided.");
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetImage(int id)
        //{
        //    var image = await _context.images.FirstOrDefaultAsync(i => i.Id == id);
        //    if (image == null) return NotFound();

        //    return File(image.Data, "image/jpeg"); // Adjust content type based on image
        //}
    }
}
