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
using Microsoft.AspNetCore.Authorization;

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListStudent")]
        public IActionResult GetListStudent([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var students = _context.Students.FromSqlRaw($"EXEC GetListStudent {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityStudent = 0;
            if (querySearch == "")
            {
                quantityStudent = _context.Students.Count();
            }
            else
            {
                quantityStudent = _context.Students.Where(s => EF.Functions.Like(s.FullName, $"%{querySearch}%") || EF.Functions.Like(s.Id, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityStudent, list = students });
        }

        [HttpGet("GetListStudentByOrganizationId")]
        public IActionResult GetListStudentByOrganizationId([FromQuery] int? id, int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var students = _context.Students.FromSqlInterpolated($"EXEC GetStudentsByOrganizationId {pageSize}, {pageNumber}, {querySearch}, {id}").ToList();
            var quantityStudent = students.Count();
            return Ok(new { count = quantityStudent, list = students });
        }

        [HttpGet("SearchStudent")]
        public IActionResult SearchStudent([FromQuery] string searchQuery, int pageSize, int pageNumber)
        {
            var students = _context.Students.FromSqlRaw($"EXEC SearchStudent N'{searchQuery}', {pageSize}, {pageNumber}").ToList();
            var quantityStudentSearch = students.Count();

            return Ok(new { count = quantityStudentSearch, list = students });
        }

        [HttpGet("GetStudentById/{id}")]
        public IActionResult GetStudentById(string id)
        {

            //var studentIdParam = new SqlParameter("@StudentId", id);

            var student = _context.Students.FromSqlRaw(
        $"GetStudentById {id}").AsEnumerable().SingleOrDefault();
            return Ok(student);

        }

        [HttpPost("AddStudent")]
        //[Authorize(Roles = "ADc")]
        public ActionResult AddStudent([FromBody] AddStudent addStudent)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddStudent {addStudent.Student_Id}, {addStudent.FullName}, {addStudent.Birthday}, {addStudent.Address}, {addStudent.Gender}, {addStudent.Email}, {addStudent.PhoneNumber}, {addStudent.Rank}, {addStudent.Course_Id}, {addStudent.Organization_Id}, {addStudent.Class_Id}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added Student Successfully.");
            }
            else
            {
                return BadRequest("Added Student Failed.");
            }
        }

        [HttpPut("UpdateStudent")]
        //[Authorize(Roles = "ADMIN")]
        public ActionResult UpdateStudent([FromBody] UpdateStudent updateStudent)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateStudent @Id={updateStudent.Student_Id}, @FullName={updateStudent.FullName}, @Birthday={updateStudent.Birthday}, @Address={updateStudent.Address}, @Gender={updateStudent.Gender}, @Email={updateStudent.Email}, @PhoneNumber={updateStudent.PhoneNumber}, @Rank={updateStudent.Rank}, @Course_Id={updateStudent.Course_Id}, @Organization_Id={updateStudent.Organization_Id}, @Class_Id={updateStudent.Class_Id}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated Student Successfully.");
            }
            else
            {
                return BadRequest("Updated Student Failed.");
            }
        }


        [HttpDelete("DeleteStudent/{id}")]
        [Authorize(Roles = "ADc")]
        public IActionResult DeleteStudent(string id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteStudent {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có sinh viên bị xóa thành công
                return Ok("Student deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể sinh viên không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("Student not found or deletion failed.");
            }
        }

        [HttpGet("GetListStudentByClassId/{id}")]
        public IActionResult GetListStudentByClassId(int id)
        {
            var students = _context.Students.FromSqlInterpolated($"EXEC GetListStudentByClass {id}").ToList();
            var quantityStudent = students.Count();
            return Ok(new { count = quantityStudent, list = students });
        }
    }
}
