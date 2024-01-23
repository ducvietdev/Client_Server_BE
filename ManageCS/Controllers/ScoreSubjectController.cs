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
    public class ScoreSubjectController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetScoreByStudentId/{id}")]
        public IActionResult GetScoreByStudentId(string id, [FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var tableScores = _context.TableScores.FromSqlInterpolated($"EXEC GetScoreByStudentId {pageSize}, {pageNumber}, {querySearch}, {id}").ToList();
            var quantityScoreStudent = 0;
            if (querySearch == "")
            {
                quantityScoreStudent = tableScores.Count();
            }
            else
            {
                quantityScoreStudent = tableScores.Where(s => s.StudentName.Contains(querySearch)).Count();
            }
            return Ok(new { count = quantityScoreStudent, list = tableScores });
        }

        [HttpGet("GetScore")]
        public IActionResult GetScore([FromQuery] int organization_id, int year_id, int semester_id, int subject_id, int class_id)
        {
            var tableScores = _context.TableScores.FromSqlInterpolated($"EXEC GetScore {organization_id}, {year_id}, {semester_id}, {subject_id}, {class_id}").ToList();
            var quantityScoreStudent = _context.TableScores.Count();
            return Ok(new { count = quantityScoreStudent, list = tableScores });
        }

        [HttpPost("AddTableScore")]
        public ActionResult AddOrganization([FromBody] AddTableScore addTableScore)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddTableScore {addTableScore.Student_Id}, {addTableScore.Subject_Id}, {addTableScore.DiemChuyenCan}, {addTableScore.DiemThuongXuyen}, {addTableScore.DiemThi}, {addTableScore.SoTinChi}, {addTableScore.Description}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added TableScore Successfully.");
            }
            else
            {
                return BadRequest("Added TableScore Failed.");
            }
        }

        [HttpPut("UpdateTableScore")]
        public ActionResult UpdateTableScore([FromBody] UpdateTableScore updateTableScore)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC UpdateTableScore {updateTableScore.Student_Id}, {updateTableScore.Subject_Id}, {updateTableScore.DiemChuyenCan}, {updateTableScore.DiemThuongXuyen}, {updateTableScore.DiemThi}, {updateTableScore.SoTinChi}, {updateTableScore.Description}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added TableScore Successfully.");
            }
            else
            {
                return BadRequest("Added TableScore Failed.");
            }
        }

        [HttpDelete("DeleteTableScore/{id}")]
        public IActionResult DeleteTableScore(int id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteTableScore {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có sinh viên bị xóa thành công
                return Ok("TableScore deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể sinh viên không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("TableScore not found or deletion failed.");
            }
        }
    }
}
    