using ManageCS.Models;
using ManageCS.Entities;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class PracticePlansController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities

        [HttpGet("GetListResultAttendance")]
        public IActionResult GetListResultAttendance([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var resultAttendance = _context.Attendances.FromSqlInterpolated($"EXEC GetListResultAttendance {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityAttendance = 0;
            if (querySearch == "")
            {
                quantityAttendance = _context.Attendances.Count();
            }
            else
            {
                quantityAttendance = _context.Attendances.Where(s => EF.Functions.Like(s.StudentName, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityAttendance, list = resultAttendance });
        }

        [HttpGet("GetListResultAttendanceById/{id}")]
        public IActionResult GetListResultAttendanceById(int id, [FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var resultAttendance = _context.Attendances.FromSqlInterpolated($"EXEC GetListResultAttendanceById {pageSize}, {pageNumber}, {querySearch}, {id}").ToList();
            var quantityAttendance = 0;
            if (querySearch == "")
            {
                quantityAttendance = _context.Attendances.Count();
            }
            else
            {
                quantityAttendance = _context.Attendances.Where(s => EF.Functions.Like(s.StudentName, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityAttendance, list = resultAttendance });
        }

        [HttpPost("MarkAttendance")]
        public IActionResult GetMarkAttendance([FromQuery] int? plan_id, int? student_id)
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<AttendanceData>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("MarkAttendance", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TrainingPlanId", plan_id);
                    command.Parameters.AddWithValue("@StudentId", student_id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //var id = (int)reader["Id"];
                            var plan_Id = (int)reader["plan_id"];
                            var student_Id = (string)reader["student_id"];
                            var buoidihoc = (int)reader["sobuoi"];
                            var comat = (double)reader["comat"];
                            var plan_name = (string)reader["plan_name"];
                            var student_name = (string)reader["student_name"];

                            var result = new AttendanceData
                            {
                                //Id = id,
                                PlanId = plan_Id,
                                StudentId = student_Id,
                                Sobuoi = buoidihoc,
                                Comat = comat,
                                PlanName = plan_name,
                                StudentName = student_name
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            return Ok(results);
        }

        [HttpGet("GetTraineesByTrainingPlanId/{plan_id}")]
        public IActionResult GetTraineesByTrainingPlanId(int? plan_id, [FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<AttendanceStatistic>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetTraineesByTrainingPlanId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TrainingPlanId", plan_id);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                    command.Parameters.AddWithValue("@SearchTerm", querySearch);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //var id = (int)reader["Id"];
                            var Id = (string)reader["id"];
                            var full_name = (string)reader["fullName"];
                            var birthday = (DateTime)reader["birthday"];
                            var class_name = (string)reader["class_name"];
                            var plan_Id = (int)reader["plan_id"];
                            var title = (string)reader["title"];
                            var sobuoi = (int)reader["sobuoi"];

                            var result = new AttendanceStatistic
                            {
                                //Id = id,
                                Id = Id,
                                FullName = full_name,
                                Birthday = birthday,
                                ClassName = class_name,
                                Plan_Id = plan_Id,
                                Title = title,
                                TongSoBuoi = sobuoi
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            return Ok( results );
        }
    }
}
