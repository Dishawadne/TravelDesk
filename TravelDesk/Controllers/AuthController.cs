using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelDesk.Models;
using TravelDesk.ViewModel;
using TravelDesk.Context;

namespace TravelDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbContexts _context;
        private readonly IConfiguration _configuration;
        public AuthController(DbContexts context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            IActionResult response = Unauthorized();
            // Authenticate the user
            var user = Authenticate(loginModel);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var role = _context.Roles.FirstOrDefault(x => x.RoleId == user.RoleId);
            if (role == null)
            {
                return Unauthorized("Role not found.");
            }
            loginModel.RoleName = role.RoleName;
            var tokenString = GenerateJSONWebToken(user, loginModel.RoleName);
            response = Ok(new
            {
                token = tokenString,
                firstName = user.FirstName,
                roleId = user.RoleId,
            });
            return response;
        }
        private string GenerateJSONWebToken(User user, String roleName)
        {
            var claims = new[]
            {
            new Claim("Email", user.Email),
           new Claim("id",user.Id.ToString()),
          new Claim("managerid", user.ManagerId?.ToString() ?? ""),
           new Claim("name",string.Concat(user.FirstName," ",user.LastName)),
             new Claim(ClaimTypes.Role, roleName),
            new Claim("roleId",user.RoleId.ToString()) 
            //new Claim("date", datetime.now.tostring())
        };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private User Authenticate(LoginModel loginModel)
        {
            return _context.Users.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);
        }
    }
}
