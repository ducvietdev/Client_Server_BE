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
    public class RoleController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListRole")]
        public IActionResult GetListRole([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var roles = _context.Roles.FromSqlInterpolated($"EXEC GetListRole {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityRole = 0;
            if (querySearch == "")
            {
                quantityRole = _context.Roles.Count();
            }
            else
            {
                quantityRole = _context.Roles.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityRole, list = roles });
        }

        [HttpDelete("DeleteRole/{id}")]
        public IActionResult DeleteRole(int? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteRole {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("Role deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("Role not found or deletion failed.");
            }
        }
    }
}
