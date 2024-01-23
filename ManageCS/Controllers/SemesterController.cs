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
    public class SemesterController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListSemesterByYearId/{id}")]
        public IActionResult GetListSemesterByYearId(int? id)
        {
            var semesters = _context.Semesters.FromSqlInterpolated($"EXEC GetListSemesterByYear {id}").ToList();
            var quantitySemesterByYear = semesters.Count();
            return Ok(new { count = quantitySemesterByYear, list = semesters });
        }
    }
}
