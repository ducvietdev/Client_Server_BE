using ManageCS.Models;
using ManageCS.Entities;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypeController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListEquipmentType")]
        public IActionResult GetListEquipmentType([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var equipmentTypes = _context.EquipmentTypes.FromSqlInterpolated($"EXEC GetListEquipmentType {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityEquipmentType = 0;
            if (querySearch == "")
            {
                quantityEquipmentType = equipmentTypes.Count();
            } 
            else
            {
                quantityEquipmentType = _context.EquipmentTypes.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityEquipmentType, list = equipmentTypes });
        }

        [HttpPost("AddEquipmentType")]
        public ActionResult AddEquipmentType([FromBody] AddEquipmentType addEquipmentType)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddEquipmentType {addEquipmentType.Code}, {addEquipmentType.Name}, {addEquipmentType.IsActive}, {addEquipmentType.Description}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added EquipmentType Successfully.");
            }
            else
            {
                return BadRequest("Added EquipmentType Failed.");
            }
        }

        [HttpGet("GetEquipmentTypeById/{id}")]
        public IActionResult GetEquipmentTypeById(int id)
        {

            //var EquipmentTypeIdParam = new SqlParameter("@EquipmentTypeId", id);

            var EquipmentType = _context.EquipmentTypes.FromSqlRaw(
        $"GetEquipmentTypeById {id}").AsEnumerable().SingleOrDefault();
            return Ok(EquipmentType);

        }

        //[HttpPut("UpdateEquipmentType/{id}")]
        //public ActionResult UpdateEquipmentType(int id, [FromBody] UpdateEquipmentType updateEquipmentType)
        //{
        //    var existingEquipmentType = _context.EquipmentTypes.Find(id);

        //    if (existingEquipmentType == null)
        //    {
        //        return NotFound();
        //    }

        //    existingEquipmentType.Code = updateEquipmentType.Code;
        //    existingEquipmentType.Name = updateEquipmentType.Name;
        //    existingEquipmentType.IsActive = updateEquipmentType.IsActive;
        //    existingEquipmentType.Description = updateEquipmentType.Description;

        //    _context.SaveChanges();

        //    return Ok("Updated equipment type successfully!");
        //}

        [HttpPut("UpdateEquipmentType")]
        public ActionResult UpdateEquipmentType([FromBody] UpdateEquipmentType updateEquipmentType)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateEquipmentType {updateEquipmentType.Id}, {updateEquipmentType.Code}, {updateEquipmentType.Name}, {updateEquipmentType.IsActive},  {updateEquipmentType.Description}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated EquipmentType Successfully.");
            }
            else
            {
                return BadRequest("Updated EquipmentType Failed.");
            }
        }

        [HttpDelete("DeleteEquipmentType/{id}")]

        public IActionResult DeleteEquipmentType(int? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteEquipmentType {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("EquipmentType deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("EquipmentType not found or deletion failed.");
            }
        }
    }
}
