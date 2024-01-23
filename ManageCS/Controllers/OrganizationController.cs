using ManageCS.Entities;
using ManageCS.Models;
using ManageCS.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Threading;
using System.Web.Http.Results;

namespace ManageCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : Controller
    {
        ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        [HttpGet("GetListOrganization")]
        public IActionResult GetListOrganization([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var organizations = _context.Organizations.FromSqlInterpolated($"EXEC GetListOrganization {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityOrganization = 0;
            if (querySearch == "")
            {
                quantityOrganization = _context.Organizations.Count();
            }
            else
            {
                quantityOrganization = _context.Organizations.Where(s => EF.Functions.Like(s.OrganizationCode, $"%{querySearch}%") || EF.Functions.Like(s.Id.ToString(), $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityOrganization, list = organizations });
        }

        [HttpGet("GetListOrganizationLevel3")]
        public IActionResult GetListOrganizationLevel3()
        {
            var organizations = _context.Organizations.FromSqlInterpolated($"EXEC GetListOrganizationLevel_3").ToList();
            return Ok(organizations);
        }

        [HttpGet("GetOrganizationHierarchy")]
        public JsonResult GetOrganizationHierarchy()
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            string JsonResult = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetOrganizationHierarchyWithIdOutside", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JsonResult = (string)reader["JsonResult"];
                        }
                    }
                }
            }
            var jsonOg = JsonConvert.DeserializeObject<OrganizationData>(JsonResult);
            return Json(jsonOg);
        }

        [HttpPost("AddOrganization")]
        public ActionResult AddOrganization([FromBody] AddOrganization addOrganization)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddOrganization {addOrganization.OrganizationCode}, {addOrganization.OrganizationName}, {addOrganization.Organization_TypeId}, {addOrganization.Organization_LevelId}, {addOrganization.Organization_ParentId}, {addOrganization.Description}, {addOrganization.Address}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added Organization Successfully.");
            }
            else
            {
                return BadRequest("Added Organization Failed.");
            }
        }

        [HttpPut("UpdateOrganization")]
        public ActionResult UpdateOrganization([FromBody] UpdateOrganization updateOrganization)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateOrganization {updateOrganization.Id}, {updateOrganization.OrganizationCode}, {updateOrganization.OrganizationName}, {updateOrganization.Organization_TypeId}, {updateOrganization.Organization_LevelId}, {updateOrganization.Organization_ParentId}, {updateOrganization.Description}, {updateOrganization.Address}");

            // Check the result
            if (result.Result < 0)
            {
                return Ok("Updated Organization Successfully.");
            }
            else
            {
                return BadRequest("Updated Organization Failed.");
            }
        }

        [HttpDelete("DeleteOrganization/{id}")]
        public IActionResult DeleteOrganization(int id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteOrganization {id}");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có sinh viên bị xóa thành công
                return Ok("Organization deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể sinh viên không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("Organization not found or deletion failed.");
            }
        }

        [HttpGet("GetHigherLevelOrganizations/{id}")]
        public IActionResult GetHigherLevelOrganizations(int id)
        {
            var organizations = _context.Organizations.FromSqlInterpolated($"EXEC GetHigherLevelUnits {id}").ToList();
            return Ok(organizations);
        }

        [HttpGet("GetOgLevelById/{id}")]
        public IActionResult GetOgLevelById(int id)
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            int result = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetOgLevelById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@organization_id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var organization_levelid = (int)reader["organization_levelId"];
                            result = organization_levelid;
                        }
                    }
                }
            }

            return Ok(result);

        }

        [HttpGet("GetOgByDoubleId")]
        public IActionResult GetOgByDoubleId([FromQuery] int? organization_id, int? organization_levelId)
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Replace with your actual connection string
            var results = new List<OrganizationLowerLevel>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("GetOgByDoubleId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@organization_id", organization_id);
                    command.Parameters.AddWithValue("@organization_levelId", organization_levelId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var organization_name = (string)reader["organization_name"];
                            var id = (int)reader["id"];
                            var result = new OrganizationLowerLevel
                            {
                                Id = id,
                                Organization_Name = organization_name
                            };
                            results.Add(result);
                        }
                    }
                }
            }

            return Ok(results);
        }

        // Đơn vị cấp trên trực tiếp và đơn vị cấp dưới trực tiếp của mình
        [HttpGet("GetSubordinatesAndSuperior/{id}")]
        public IActionResult GetSubordinatesAndSuperior(int id)
        {
            var organizations = _context.Organizations.FromSqlInterpolated($"EXEC GetSubordinatesAndSuperior {id}").ToList();
            var quantityOrganization = organizations.Count();
            return Ok(new { count = quantityOrganization, list = organizations });
        }

        // Lấy đơn vị mình và cấp dưới
        [HttpGet("GetOgAndSubOgs/{id}")]
        public IActionResult GetOgAndSubOgs(int? id, [FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var organizations = _context.Organizations.FromSqlInterpolated($"EXEC GetOgAndSubOgs {id}, {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityOrganization = organizations.Count();
            return Ok(new { count = quantityOrganization, list = organizations });
        }

        // Lấy đv cấp tren trực tiếp và đơn vị cấp dưới tất cả
        [HttpGet("GetParentOgAndOgAndSubOgs/{id}")]
        public IActionResult GetParentOgAndOgAndSubOgs(int? id, [FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var organizations = _context.Organizations.FromSqlInterpolated($"EXEC GetParentOgAndOgAndSubOgs {id}, {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityOrganization = organizations.Count();
            return Ok(new { count = quantityOrganization, list = organizations });
        }
    }
}
