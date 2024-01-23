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
    public class SubjectController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListSubject")]
        public IActionResult GetListSubject([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var Subjects = _context.Subjects.FromSqlInterpolated($"EXEC GetListSubject {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantitySubject = 0;
            if (querySearch == "")
            {
                quantitySubject = _context.Subjects.Count();
            }
            else
            {
                quantitySubject = _context.Subjects.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantitySubject, list = Subjects });
        }

        [HttpGet("GetListSubjectBySemesterId/{id}")]
        public IActionResult GetListSubjectBySemesterId(int? id)
        {
            var subjects = _context.Subjects.FromSqlInterpolated($"EXEC GetListSubjectBySemester {id}").ToList();
            var quantitySubjectByYear = subjects.Count();
            return Ok(new { count = quantitySubjectByYear, list = subjects });
        }
    }
}
