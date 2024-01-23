using ManageCS.Models;
using ManageCS.Entities;
using Microsoft.AspNetCore.Mvc;
using ManageCS.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedStudentController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        Support.QueryNotEntity queryNotEntity = new Support.QueryNotEntity();
        private readonly IConfiguration _configuration;

        public ClassifiedStudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("GetStudentScoresWithDynamicSubjects")]
        public IActionResult GetStudentScoresWithDynamicSubjects([FromQuery] int year_id, int semester_id, int organization_id)
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<AverageSubject>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetStudentScoresWithDynamicSubjects", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if your stored procedure has input parameters
                    command.Parameters.AddWithValue("@NamHoc", year_id);
                    command.Parameters.AddWithValue("@KyHoc", semester_id);
                    command.Parameters.AddWithValue("@Unit_Id", organization_id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (string)reader["id"];
                            var studentId = (string)reader["student_id"];
                            var studentName = (string)reader["student_name"];
                            var birthday = (DateTime)reader["birthday"];
                            var subject1 = (double)reader["Tiếng Anh"];
                            var subject2 = (double)reader["Hiệu năng mạng"];
                            var subject3 = (double)reader["Quản trị mạng"];
                            //var subject4 = (string)reader["student_name"];
                            //var subject5 = (string)reader["student_name"];


                            var result = new AverageSubject
                            {
                                Id = id,
                                StudentId = studentId,
                                StudentName = studentName,
                                Birthday = birthday,
                                Subject1 = subject1,
                                Subject2 = subject2,
                                Subject3 = subject3
                                //Subject4 = classification,
                                //Subject5 = classification,
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            return Ok(results);
        }


        [HttpGet("GetStudentAverageScore")]
        public IActionResult GetStudentAverageScore([FromQuery] int year_id, int semester_id, int organization_id)
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<AverageScore>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetStudentAverageScore", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if your stored procedure has input parameters
                    command.Parameters.AddWithValue("@NamHoc", year_id);
                    command.Parameters.AddWithValue("@KyHoc", semester_id);
                    command.Parameters.AddWithValue("@UnitId", organization_id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (string)reader["id"];
                            var studentId = (string)reader["student_id"];
                            var birthday = (DateTime)reader["birthday"];
                            var averageScore = (decimal)reader["diemtbchung"];
                            var classification = (string)reader["xeploai"];

                            var result = new AverageScore
                            {
                                Id = id,
                                StudentId = studentId,
                                Birthday = birthday,
                                AverageScore2 = averageScore,
                                ClassifiedStudent = classification
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
