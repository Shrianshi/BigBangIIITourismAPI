using BigBangIII_Api.Models;
using BigBangIII_Api.Reqirements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BigBangIII_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public TokenController(IConfiguration config, ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(loginmodel _userData)
        {
            if (_userData != null && _userData.Username != null && _userData.Password != null )
            {
                var user = await GetUser(_userData.Username, _userData.Password);
                var status = await GetStatus(user.UserId);

                if (user != null && status.Status == "enabled")
                {
                    //var status = await GetStatus(user.Status);
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                         new Claim("UserId", user.UserId.ToString()),
                         new Claim("Email", user.UserEmail),
                        new Claim("Password",user.Password),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("Status", status.Status)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else
                {
                    return BadRequest("User status not enabled");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string username, string password)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
        private async Task<User> GetStatus(int statusId)
        {
            return await _context.users.FirstOrDefaultAsync(s => s.UserId == statusId);
        }
        //private async Task<Agent> GetStatus(string status)
        //{
        //    return await _context.agents.FirstOrDefaultAsync(s => s.Status == status);
        //}

        //private async Task<string> GetStatus(int Id)
        //{
        //    var status = await _context.agents.FindAsync(Id);
        //    return status.Status;
        //}
    }
}
