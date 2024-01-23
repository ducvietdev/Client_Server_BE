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
    public class TrainingPlanController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListTrainingPlan")]
        public IActionResult GetListTrainingPlan([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var trainingPlans = _context.TrainingPlans.FromSqlInterpolated($"EXEC GetListTrainingPlan {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityTrainingPlan = 0;
            if (querySearch == "")
            {
                quantityTrainingPlan = trainingPlans.Count();
            }
            else
            {
                quantityTrainingPlan = _context.TrainingPlans.Where(s => EF.Functions.Like(s.Location, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityTrainingPlan, list = trainingPlans });
        }

        [HttpGet("GetTrainingPlanById")]
        public IActionResult GetTrainingPlanById([FromQuery] int? id = null)
        {
            var trainingPlans = _context.TrainingPlans.FromSqlInterpolated($"EXEC GetTrainingPlans {id}").ToList();
            var quantityPlans = trainingPlans.Count();
            return Ok(new { count = quantityPlans, list = trainingPlans });
        }

        [HttpPost("AddTrainingPlan")]
        public ActionResult AddTrainingPlan([FromBody] AddTrainingPlan addTrainingPlan)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddTrainingPlan {addTrainingPlan.Title}, {addTrainingPlan.SoTiet}, {addTrainingPlan.Start}, {addTrainingPlan.TimeEnd}, {addTrainingPlan.Location}, {addTrainingPlan.Description}, {addTrainingPlan.TongSoBuoi}, {addTrainingPlan.Year_Id}, {addTrainingPlan.Type_Id}, {addTrainingPlan.Semester_Id}, {addTrainingPlan.Organization_Id}, {addTrainingPlan.Equipment_Id}, {addTrainingPlan.Subject_Id}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added TrainingPlan Successfully.");
            }
            else
            {
                return BadRequest("Added TrainingPlan Failed.");
            }
        }

        [HttpPut("UpdateTrainingPlan")]
        public ActionResult UpdateTrainingPlan([FromBody] UpdateTrainingPlan updateTrainingPlan)
        {

            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                 $"EXEC UpdateTrainingPlan {updateTrainingPlan.Id}, {updateTrainingPlan.Title}, {updateTrainingPlan.SoTiet}, {updateTrainingPlan.Start}, {updateTrainingPlan.TimeEnd}, {updateTrainingPlan.Location}, {updateTrainingPlan.Description}, {updateTrainingPlan.TongSoBuoi}, {updateTrainingPlan.Year_Id}, {updateTrainingPlan.Type_Id}, {updateTrainingPlan.Semester_Id}, {updateTrainingPlan.Organization_Id}, {updateTrainingPlan.Equipment_Id}, {updateTrainingPlan.Subject_Id}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated TrainingPlan Successfully.");
            }
            else
            {
                return BadRequest("Updated TrainingPlan Failed.");
            }
        }

        [HttpDelete("DeleteTrainingPlan/{id}")]
        public IActionResult DeleteTrainingPlan(int? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteTrainingPlan {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("TrainingPlan deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("TrainingPlan not found or deletion failed.");
            }
        }

        [HttpGet("GetListTrainingPlanByOrganizationId/{id}")]
        public IActionResult GetListTrainingPlanByOrganizationId(int? id, [FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var trainingPlans = _context.TrainingPlans.FromSqlInterpolated($"EXEC GetListTrainingPlanByOrganizationId {pageSize}, {pageNumber}, {querySearch}, {id}").ToList();
            var quantityTrainingPlan = trainingPlans.Count();
            return Ok(new { count = quantityTrainingPlan, list = trainingPlans });
        }
    }
}
