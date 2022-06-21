using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly AppDbContext _context;

        public LoginController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AppUser login)
        {
            try
            {
                var user = _context.AppUsers.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);
                if (user == null) return BadRequest(new { message = "Wrong Username or Password" });
                
                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { message = "Token successfully created", accessToken = tokenString, username = user.Username });

                
            }
            catch (Exception e)
            {
                //logger here. We dont want to sent the Exception Message to the user.
                return StatusCode(500, new { message = e.Message });
            }
        }

        private string GenerateJSONWebToken(AppUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              GetClaims(userInfo),
              expires: DateTime.Now.AddHours(20),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static IEnumerable<Claim> GetClaims(AppUser userInfo)
        {
            IEnumerable<Claim> claims = new Claim[] {
                new Claim("Id", userInfo.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userInfo.Username),
                    new Claim(ClaimTypes.Email, userInfo.Email),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(20).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
    }
}