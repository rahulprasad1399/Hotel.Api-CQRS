using Hotel.Application.RefreshTokenResponse;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
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
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<RefreshTokenResponseDto>>
    {
        private readonly HotelContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommandHandler(HotelContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ApiResponse<RefreshTokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var employee = await ValidateRefrehToken(request.UserId, request.RefreshToken);
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

        private async Task<Employee> ValidateRefrehToken(int id, string refreshToken)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null || (employee.RefreshToken != refreshToken) || (employee.RefreshTokenExpiryTime <= DateTime.UtcNow))
            {
                return null;
            }
            return employee;
        }
    }
}
