using ManageCS.Entities;
using ManageCS.Models;
using ManageCS.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading;
using System.Web.Http.Results;

namespace ManageCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorTypeController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities

        [HttpGet("GetListMonitorType")]
        public IActionResult GetListMonitorType()
        {
            var monitorTypes = _context.MonitorTypes.FromSqlInterpolated($"EXEC GetListMonitorType").ToList();
            return Ok(monitorTypes);
        }


    }
}
