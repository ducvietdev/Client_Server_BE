using ManageCS.Models;
using ManageCS.Entities;
using Microsoft.AspNetCore.Mvc;
using ManageCS.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetTotalStudentsByAllCourses")]
        public IActionResult GetTotalStudentsByAllCourses()
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<StatisticStudentByCourse>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetTotalStudentsByAllCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["id"];
                            var course_name = (string)reader["course"];
                            var quantity = (int)reader["total_students"];

                            var result = new StatisticStudentByCourse
                            {
                                Id = id,
                                Course_Name = course_name,
                                Quantity = quantity
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            return Ok(results);
        }

        [HttpGet("GetTotalMonitorsByAllCourses")]
        public IActionResult GetTotalMonitorsByAllCourses()
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<StatisticMonitorByCourse>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetTotalMonitorsByAllCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["id"];
                            var course_name = (string)reader["course"];
                            var quantity = (int)reader["total_monitors"];

                            var result = new StatisticMonitorByCourse
                            {
                                Id = id,
                                Course_Name = course_name,
                                Quantity = quantity
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            return Ok(results);
        }
    }
}
