using ManageCS.Models;
using ManageCS.Entities;
using Microsoft.AspNetCore.Mvc;
using ManageCS.Support;
using Microsoft.EntityFrameworkCore;


namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListEquipment")]
        public IActionResult GetListEquipment([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var equipments = _context.Equipment.FromSqlInterpolated($"EXEC GetListEquipment {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityEquipment = 0;
            if (querySearch == "")
            {
                quantityEquipment = _context.Equipment.Count();
            }
            else
            {
                quantityEquipment = _context.Equipment.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityEquipment, list = equipments });
        }

        [HttpGet("GetListEquipmentByOrganizationId")]
        public IActionResult GetListEquipmentByOrganizationId([FromQuery] int? id, int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var equipments = _context.Equipment.FromSqlInterpolated($"EXEC GetEquipmentsByOrganizationId {pageSize}, {pageNumber}, {querySearch}, {id}").ToList();
            var quantityStudent = equipments.Count();
           
            return Ok(new { count = quantityStudent, list = equipments });
        }

        [HttpPost("AddEquipment")]
        public ActionResult AddEquipment([FromBody] AddEquipment addEquipment)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddEquipment {addEquipment.Code}, {addEquipment.Name}, {addEquipment.Quality}, {addEquipment.YearUse}, {addEquipment.Equipment_UnitId}, {addEquipment.Equipment_TypeId}, {addEquipment.Organization_Id}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added Equipment Successfully.");
            }
            else
            {
                return BadRequest("Added Equipment Failed.");
            }
        }

        [HttpPut("UpdateEquipment")]
        public ActionResult UpdateEquipment([FromBody] UpdateEquipment updateEquipment)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateEquipment {updateEquipment.Id}, {updateEquipment.Code}, {updateEquipment.Name}, {updateEquipment.Quality}, {updateEquipment.YearUse}, {updateEquipment.Equipment_UnitId},  {updateEquipment.Equipment_TypeId}, {updateEquipment.Organization_Id}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated Equipment Successfully.");
            }
            else
            {
                return BadRequest("Updated Equipment Failed.");
            }
        }

        [HttpDelete("DeleteEquipment/{id}")]
        public IActionResult DeleteEquipment(int? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteEquipment {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("Equipment deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("Equipment not found or deletion failed.");
            }
        }
    }
}
