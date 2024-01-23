using ManageCS.Models;
using Microsoft.AspNetCore.Mvc;
using ManageCS.Support;
using Microsoft.EntityFrameworkCore;


namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListState")]
        public IActionResult GetListState()
        {
            var states = _context.UserStates.FromSqlRaw($"EXEC GetListState").ToList();
            return Ok(states);
        }
    }
}
