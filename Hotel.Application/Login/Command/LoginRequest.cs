using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Hotel.Application.LoginRequest
{
    public class LoginRequestCommand : IRequest<ApiResponse<LoginResponseModel>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, ApiResponse<LoginResponseModel>>
    {
        private readonly HotelContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginRequestCommandHandler(HotelContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<LoginResponseModel>> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return ApiResponse<LoginResponseModel>.Fail("Please Fill all the required fields");
            }

            var userAccount = await _context.employees.FirstOrDefaultAsync((x) => x.Email == request.Email);

            if (userAccount == null)
            {
                return ApiResponse<LoginResponseModel>.Fail("Employee dosent exist with the provided Email");
            }

            if (new PasswordHasher<Employee>().VerifyHashedPassword(userAccount, userAccount.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return ApiResponse<LoginResponseModel>.Fail("Password dosen't Match");
            }

            var refreshToken = await GenerateAndSaveRefreshToken(userAccount);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userAccount.Email),
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                new Claim(ClaimTypes.Role, userAccount.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: cred
             );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                httpContext.Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Path = "/"
                });

                httpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
            }



            LoginResponseModel loginResponseModel = new LoginResponseModel();
            loginResponseModel.Email = request.Email;
            loginResponseModel.FullName = userAccount.FullName;
            loginResponseModel.Role = userAccount.Role;

            return ApiResponse<LoginResponseModel>.Ok(loginResponseModel);
        }

        private string GenerateRefreshToken()  
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshToken(Employee employee)
        {
            var refreshToken = GenerateRefreshToken();
            employee.RefreshToken = refreshToken;
            employee.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();
            return refreshToken;
        }
    }
}
