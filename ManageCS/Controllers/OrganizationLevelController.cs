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
    public class OrganizationLevelController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListOrganizationLevel")]
        public IActionResult GetListOrganizationLevel()
        {
            var organizationLevels = _context.OrganizationLevels.FromSqlInterpolated($"EXEC GetListOrganizationLevel").ToList();
            var quatityOrganizationLevel = organizationLevels.Count();
            return Ok(new { count = quatityOrganizationLevel, list = organizationLevels });
        }

        [HttpGet("GetListLevelByOrganizationId/{id}")]
        public IActionResult GetListLevelByOrganizationId(int id)
        {
            var level = _context.OrganizationLevels.FromSqlInterpolated($"EXEC GetListLevelByOrganizationId {id}").AsEnumerable().SingleOrDefault();
            return Ok(level);
        }
    }
}
