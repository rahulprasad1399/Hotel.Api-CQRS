using Hotel.Application.RefreshTokenResponse;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Hotel.Application.RefreshToken
{
    public class RefreshTokenCommand : IRequest<ApiResponse<RefreshTokenResponseDto>>
    {
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<RefreshTokenResponseDto>>
    {
        private readonly HotelContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RefreshTokenCommandHandler(HotelContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<RefreshTokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var httpContext = _httpContextAccessor.HttpContext;
            var refreshToken = httpContext.Request.Cookies["refreshToken"];

            if(string.IsNullOrEmpty(refreshToken))
            {
                return ApiResponse<RefreshTokenResponseDto>.Fail("Refresh token missing");
            }

            var employee = await ValidateRefrehToken(refreshToken);
            if (employee == null)
            {
                return ApiResponse<RefreshTokenResponseDto>.Fail("Invalid or Expired Refresh Token");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role),
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var newRefreshToken = await GenerateAndSaveRefreshToken(employee);

            httpContext.Response.Cookies.Append("token", newAccessToken, new CookieOptions
            {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddMinutes(30)
            });

            httpContext.Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly= true,
                Secure = true,  
                SameSite = SameSiteMode.None,
                Expires= DateTime.UtcNow.AddDays(7)
            });

            var response = new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return ApiResponse<RefreshTokenResponseDto>.Ok(response);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
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

        private async Task<Employee> ValidateRefrehToken(string refreshToken)
        {
            return await _context.employees.FirstOrDefaultAsync(e =>
                e.RefreshToken == refreshToken &&
                e.RefreshTokenExpiryTime > DateTime.UtcNow
            );
        }
    }
}
