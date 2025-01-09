using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolemasterController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public RolemasterController(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] Rolemaster rolemasterObj)
        {
            if (rolemasterObj == null)
                return BadRequest(new { Message = "Invalid role data!" });

            if (string.IsNullOrWhiteSpace(rolemasterObj.RoleCode) || string.IsNullOrWhiteSpace(rolemasterObj.RoleName))
                return BadRequest(new { Message = "RoleCode and RoleName are required!" });

            try
            {
                await _authContext.rolemasters.AddAsync(rolemasterObj);
                await _authContext.SaveChangesAsync();

                return Ok(new { Message = "Role added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.InnerException?.Message ?? ex.Message });
            }
        }

    }
}
