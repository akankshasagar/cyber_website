using CyberSecurity_new.Context;
using CyberSecurity_new.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberSecurity_new.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public DepartmentController(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost("AddDept")]
        public async Task<IActionResult> AddRole([FromBody] Department departmentObj)
        {
            if (departmentObj == null)
                return BadRequest(new { Message = "Invalid department data!" });

            if (string.IsNullOrWhiteSpace(departmentObj.DeptCode) || string.IsNullOrWhiteSpace(departmentObj.DeptName))
                return BadRequest(new { Message = "RoleCode and RoleName are required!" });

            try
            {
                await _authContext.Departments.AddAsync(departmentObj);
                await _authContext.SaveChangesAsync();

                return Ok(new { Message = "Department added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _authContext.Departments
                .Select(d => new { d.DeptId, d.DeptName })
                .ToListAsync();

            return Ok(departments);
        }


    }
}
