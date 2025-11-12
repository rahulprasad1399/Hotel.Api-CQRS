using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HotelContext _context;
        private readonly IConfiguration configuration;
        public AuthController(HotelContext context, IConfiguration config)
        {
            _context = context;
            configuration = config;
        }
        public static Employee employee = new Employee();

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequestModel user)
        {
            var hashedPassword = new PasswordHasher<Employee>()
                .HashPassword(employee, user.Password);

            employee.FullName = user.FullName;
            employee.Role = user.Role;
            employee.Email = user.Email;
            employee.Password = hashedPassword;
            employee.HotelId = null;

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAdmin(LoginRequestModel user)
        {
            if(employee.Email != user.Email)
            {
                return BadRequest("User Not Found");
            }

            if(new PasswordHasher<Employee>().VerifyHashedPassword(employee, employee.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(employee);

            return Ok(token);
        }

        private string CreateToken(Employee employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, employee.Email)
            };

            // dotnet add package System.IdentityModel.Tokens.Jwt
            // Need to install above mentioned package for using symmetric security key 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer : configuration.GetValue<string>("AppSettings:Issuer"),
                audience : configuration.GetValue<string>("AppSettings:Audience"),
                claims : claims,
                expires : DateTime.UtcNow.AddDays(1),
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
