using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public LoginRequestCommandHandler(HotelContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userAccount.Email),
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: cred
             );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            LoginResponseModel loginResponseModel = new LoginResponseModel();
            loginResponseModel.Token = token;   
            loginResponseModel.Email = request.Email;
            loginResponseModel.FullName = userAccount.FullName;
            loginResponseModel.Role = userAccount.Role; 

            return ApiResponse<LoginResponseModel>.Ok(loginResponseModel);
        }
    }
}
