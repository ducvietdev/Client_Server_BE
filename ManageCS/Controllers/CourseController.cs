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
    public class CourseController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities

        [HttpGet("GetListCourse")]
        public IActionResult GetListCourse()
        {
            var courses = _context.Courses.FromSqlInterpolated($"EXEC GetListCourse").ToList();
            return Ok(courses);
        }

        
    }
}
