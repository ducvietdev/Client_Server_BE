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

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListMonitor")]
        public IActionResult GetListMonitor([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var Monitors = _context.Monitors.FromSqlRaw($"EXEC GetListMonitor {pageSize}, {pageNumber}, N'{querySearch}'").ToList();
            var quantityMonitor = 0;
            if (querySearch == "")
            {
                quantityMonitor = _context.Monitors.Count();
            }
            else
            {
                quantityMonitor = _context.Monitors.Where(s => EF.Functions.Like(s.FullName, $"%{querySearch}%") || EF.Functions.Like(s.Id, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityMonitor, list = Monitors });
        }

        [HttpGet("GetListMonitorByOrganizationId")]
        public IActionResult GetListMonitorByOrganizationId([FromQuery] int id, int pageSize, int pageNumber)
        {
            var Monitors = _context.Monitors.FromSqlRaw($"EXEC GetMonitorsByOrganizationId {pageSize}, {pageNumber}, {id}").ToList();
            var quantityMonitor = Monitors.Count();
            return Ok(new { count = quantityMonitor, list = Monitors });
        }

        [HttpGet("GetMonitorById/{id}")]
        public IActionResult GetMonitorById(string id)
        {

            //var MonitorIdParam = new SqlParameter("@MonitorId", id);

            var Monitor = _context.Monitors.FromSqlRaw(
        $"GetMonitorById {id}").AsEnumerable().SingleOrDefault();
            return Ok(Monitor);

        }

        [HttpPost("AddMonitor")]
        public ActionResult AddMonitor([FromBody] AddMonitor addMonitor)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddMonitor {addMonitor.Monitor_Id}, {addMonitor.FullName}, {addMonitor.Birthday}, {addMonitor.Address}, {addMonitor.Gender}, {addMonitor.Email}, {addMonitor.PhoneNumber}, {addMonitor.Rank}, {addMonitor.Course_Id}, {addMonitor.Organization_Id}, {addMonitor.Class_Id}, {addMonitor.Monitor_TypeId}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added Monitor Successfully.");
            }
            else
            {
                return BadRequest("Added Monitor Failed.");
            }
        }

        [HttpPut("UpdateMonitor")]
        public ActionResult UpdateMonitor([FromBody] UpdateMonitor updateMonitor)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateMonitor @Id={updateMonitor.Monitor_Id}, @FullName={updateMonitor.FullName}, @Birthday={updateMonitor.Birthday}, @Address={updateMonitor.Address}, @Gender={updateMonitor.Gender}, @Email={updateMonitor.Email}, @PhoneNumber={updateMonitor.PhoneNumber}, @Rank={updateMonitor.Rank}, @Course_Id={updateMonitor.Course_Id}, @Organization_Id={updateMonitor.Organization_Id}, @Class_Id={updateMonitor.Class_Id}, @Monitor_TypeId={updateMonitor.Monitor_TypeId}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated Monitor Successfully.");
            }
            else
            {
                return BadRequest("Updated Monitor Failed.");
            }
        }


        [HttpDelete("DeleteMonitor/{id}")]
        public IActionResult DeleteMonitor(string id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteMonitor {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có sinh viên bị xóa thành công
                return Ok("Monitor deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể sinh viên không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("Monitor not found or deletion failed.");
            }
        }
    }
}
