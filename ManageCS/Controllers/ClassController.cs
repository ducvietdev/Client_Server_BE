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
    public class ClassController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities

        [HttpGet("GetListClassByOrganizationId/{id}")]
        public IActionResult GetListClassByOrganizationId(int id)
        {
            var class_ = _context.Classes.FromSqlInterpolated($"EXEC GetListClassByOrganization {id}").ToList();
            var quantityClassByOrganization = class_.Count();
            return Ok(new { count = quantityClassByOrganization, list = class_ });
        }
    }
}
