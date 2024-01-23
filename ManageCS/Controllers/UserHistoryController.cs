using ManageCS.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class UserHistoryController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("UserHistory")]
        public IActionResult HistoryUserLogin([FromQuery] int? pageSize = 10, int? pageNumber = 1)
        {
            IQueryable<UserHistory> query = _context.UserHistories.OrderByDescending(m => m.ActionTime);
            var pagedList = query.ToPagedList(pageNumber.Value, pageSize.Value);
            var quantityHistory = query.Count();
            return Ok(new { count = quantityHistory, list = pagedList });
        }

        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAll()
        {
            var items = _context.UserHistories.ToList();
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }

            _context.UserHistories.RemoveRange(items);
            _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
