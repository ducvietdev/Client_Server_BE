using ManageCS.Models;
using ManageCS.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ManageCS.Support;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace ManageCS.Controllers
{
    // Viết theo chuẩn CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : Controller
    {
      ManageCSContext _context = new ManageCSContext(); // Dùng cho Entities
        AppSetting _appSettings = new AppSetting();
        Support.Support support = new Support.Support();
        Support.QueryNotEntity queryNotEntity = new Support.QueryNotEntity();
        string query = "";
        string formattedDateTime = "";

        public UserLoginController(IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpGet("GetListUserLogin")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetListUserLogin([FromQuery] int pageSize = 10, int pageNumber = 1, string? querySearch = "")
        {
            var userLogins = _context.UserLogins.FromSqlInterpolated($"EXEC GetListUserLogin {pageSize}, {pageNumber}, {querySearch}").ToList();
            var quantityUserLogin = 0;
            if (querySearch == "")
            {
                quantityUserLogin = _context.UserLogins.Count();
            }
            else
            {
                quantityUserLogin = _context.UserLogins.Where(s => EF.Functions.Like(s.FullName, $"%{querySearch}%") || EF.Functions.Like(s.UserName, $"%{querySearch}%")).Count();
            }
            return Ok(new { count = quantityUserLogin, list = userLogins });
        }

        [HttpPost("AddUserLogin")]
        
        public ActionResult AddUserLogin([FromBody] AddUserLogin addUserLogin)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC AddUserLogin {addUserLogin.User_Id}, {addUserLogin.CreditCard}, {addUserLogin.FullName}, {addUserLogin.UserName}, {addUserLogin.Password}, {addUserLogin.State_Id}, {addUserLogin.Organization_Id}, {addUserLogin.Role_Id}, {addUserLogin.Level_Id}");
            // Nếu có thông điệp kết quả, thành công
            if (result.Result < 0)
            {
                return Ok("Added UserLogin Successfully.");
            }
            else
            {
                return BadRequest("Added UserLogin Failed.");
            }
        }

        [HttpPut("UpdateUserLogin")]
        public ActionResult UpdateUserLogin([FromBody] UpdateUserLogin updateUserLogin)
        {
            var result = _context.Database.ExecuteSqlInterpolatedAsync($"EXEC UpdateUserLogin {updateUserLogin.Id}, {updateUserLogin.CreditCard}, {updateUserLogin.FullName}, {updateUserLogin.UserName}, {updateUserLogin.Password}, {updateUserLogin.State_Id}, {updateUserLogin.Organization_Id}, {updateUserLogin.Role_Id}, {updateUserLogin.Level_Id}");
            if (result.Result < 0)
            {
                return Ok("Updated UserLogin Successfully.");
            }
            else
            {
                return BadRequest("Updated UserLogin Failed.");
            }
        }

        [HttpDelete("DeleteUserLogin/{id}")]
        public IActionResult DeleteUserLogin(string? id)
        {
            // Lấy thông điệp kết quả từ stored procedure
            var result = _context.Database.ExecuteSqlRawAsync($"EXEC DeleteUserLogin '{id}'");
            if (result.Result < 0)
            {
                // Nếu result < 0, có nghĩa là có vai trò bị xóa thành công
                return Ok("UserLogin deleted successfully.");
            }
            else
            {
                // Nếu result >= 0, có thể vai trò không tồn tại hoặc có lỗi khác xảy ra
                return NotFound("UserLogin not found or deletion failed.");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignIn login)
        {
            if (support.CheckIsEmail(login.Username)) // đăng nhập bằng email
            {
                var user = _context.UserLogins.SingleOrDefault(p => p.UserName == login.Username && p.Password == login.Password);
                UserHistory lichSuDangNhapUser = new UserHistory();
                if (user == null) //không đúng
                {
                    formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    query = "INSERT [dbo].[UserHistory] ([user_id], [action_time], [action_name], [action_content], [state]) " +
                        "VALUES (NULL, CAST(N'" + formattedDateTime + "' AS DateTime), NULL, 'Invalid username/password', 0)";
                    queryNotEntity.Query(query);

                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Invalid username/password"
                    });
                }
                else
                {
                    //cấp token
                    var token = await GenerateToken(user);

                    ////role
                    //var user_login = _context.UserLogins.SingleOrDefault(m => m.Id == user.Id);

                    formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    query = "INSERT [dbo].[UserHistory] ([user_id], [action_time], [action_name], [action_content], [state]) " +
                        "VALUES (N'" + user.Id + "', CAST(N'" + formattedDateTime + "' AS DateTime), NULL, N'Đăng nhập thành công', 1)";
                    queryNotEntity.Query(query);

                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Authenticate success",
                        Data = token,
                        DataInfo = new InfoResponse
                        {
                            Id = user.Id,
                            Username = login.Username,
                            FullName = user.FullName,
                            OrganizationId = user.OrganizationId,
                            OrganizationName = user.OrganizationName,
                            RoleId = user.RoleId,
                            RoleName = user.RoleName,
                            LevelId = user.LevelId,
                            LevelName = user.LevelName
                        }
                    });
                }
            }
            else
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Not found student",
                });
            }
        }
        private async Task<Token> GenerateToken(UserLogin userLogin)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userLogin.FullName),
                    new Claim(JwtRegisteredClaimNames.Sub, userLogin.OrganizationId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, userLogin.Id),
                    new Claim(JwtRegisteredClaimNames.Email, userLogin.UserName),
                    //new Claim("FullName", userLogin.FullName),
                    //new Claim("Id", userLogin.Id.ToString()),
                    new Claim("Role", userLogin.RoleName),
                    new Claim(ClaimTypes.Role, userLogin.RoleName),

                    //roles
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            ////Lưu database
            var user = _context.UserLogins.SingleOrDefault(x => x.Id == userLogin.Id);
            if (user != null)
            {
                user.ResetToken = refreshToken;
                _context.Update(user);
            }

            await _context.SaveChangesAsync();
            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(Token model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false //ko kiểm tra token hết hạn
            };
            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)//false
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid token"
                        });
                    }
                }

                //check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate < DateTime.UtcNow)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Access token has expired"
                    });
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _context.UserLogins.FirstOrDefault(x => x.ResetToken == model.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token does not exist"
                    });
                }

                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //create new token
                var user = await _context.UserLogins.SingleOrDefaultAsync(nd => nd.Id == storedToken.Id);
                var token = await GenerateToken(user);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Renew token success",
                    Data = token
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Something went wrong"
                });
            }
        }
        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            var resultDateTime = dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return resultDateTime;
        }
    }
}
