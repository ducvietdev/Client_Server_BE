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
    public class EquipmentUnitController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListEquipmentUnit")]
        public IActionResult GetListEquipmentUnit([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var equipmentUnits = _context.EquipmentUnits.FromSqlInterpolated($"EXEC GetListEquipmentUnit {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityEquipmentUnit = 0;
            if (querySearch == "")
            {
                quantityEquipmentUnit = _context.EquipmentUnits.Count();
            }
            else
            {
                quantityEquipmentUnit = _context.EquipmentUnits.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityEquipmentUnit, list = equipmentUnits });
        }

        [HttpDelete("DeleteEquipmentUnit/{id}")]
        public IActionResult DeleteEquipmentUnit(int? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteEquipmentUnit {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("EquipmentUnit deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("EquipmentUnit not found or deletion failed.");
            }
        }
    }
}
