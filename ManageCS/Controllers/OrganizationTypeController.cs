using ManageCS.Models;
using ManageCS.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ManageCS.Support;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading;


namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationTypeController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListOrganizationType")]
        public IActionResult GetListOrganizationType([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var OrganizationTypes = _context.OrganizationTypes.FromSqlInterpolated($"EXEC GetListOrganizationType {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityOrganizationType = 0;
            if (querySearch == "")
            {
                quantityOrganizationType = _context.OrganizationTypes.Count();
            }
            else
            {
                quantityOrganizationType = _context.OrganizationTypes.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityOrganizationType, list = OrganizationTypes });
        }

        [HttpPost("AddOrganizationType")]
        public ActionResult AddOrganizationType([FromBody] AddOrganizationType addOrganizationType)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddOrganizationType {addOrganizationType.Name}, {addOrganizationType.Description}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added OrganizationType Successfully.");
            }
            else
            {
                return BadRequest("Added OrganizationType Failed.");
            }
        }

        [HttpGet("GetOrganizationTypeById/{id}")]
        public IActionResult GetOrganizationTypeById(int id)
        {

            //var OrganizationTypeIdParam = new SqlParameter("@OrganizationTypeId", id);

            var OrganizationType = _context.OrganizationTypes.FromSqlRaw(
        $"GetOrganizationTypeById {id}").AsEnumerable().SingleOrDefault();
            return Ok(OrganizationType);

        }

        [HttpPut("UpdateOrganizationType")]
        public ActionResult UpdateOrganizationType([FromBody] UpdateOrganizationType updateOrganizationType)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateOrganizationType {updateOrganizationType.Id}, {updateOrganizationType.Name}, {updateOrganizationType.Description}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated OrganizationType Successfully.");
            }
            else
            {
                return BadRequest("Updated OrganizationType Failed.");
            }
        }

        [HttpDelete("DeleteOrganizationType/{id}")]
        public IActionResult DeleteOrganizationType(int? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteOrganizationType {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("OrganizationType deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("OrganizationType not found or deletion failed.");
            }
        }
    }
}
