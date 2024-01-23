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
using System.Threading;


namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class PlanTypeController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListPlanType")]
        public IActionResult GetListPlanType([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var PlanTypes = _context.PlanTypes.FromSqlInterpolated($"EXEC GetListPlanType {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityPlanType = 0;
            if (querySearch == "")
            {
                quantityPlanType = _context.PlanTypes.Count();
            }
            else
            {
                quantityPlanType = _context.PlanTypes.Where(s => EF.Functions.Like(s.Name, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityPlanType, list = PlanTypes });
        }
    }
}
