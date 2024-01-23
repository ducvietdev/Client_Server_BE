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
    public class YearController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListYear")]
        public IActionResult GetListYear()
        {
            var years = _context.Years.FromSqlInterpolated($"EXEC GetListYear").ToList();
            var quantityYear = years.Count();
            return Ok(new { count = quantityYear, list = years });
        }
    }
}
