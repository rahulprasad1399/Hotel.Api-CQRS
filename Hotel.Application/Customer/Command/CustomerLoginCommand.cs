using Hotel.Application.CustomerLoginResponseDto;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Application.CustomerLogin
{
    public class CustomerLoginCommand : IRequest<CustomerLoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CustomerLoginCommandHandler : IRequestHandler<CustomerLoginCommand, CustomerLoginResponse>
    {
        private readonly HotelContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        public CustomerLoginCommandHandler(HotelContext hotelContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = hotelContext;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<CustomerLoginResponse> Handle(CustomerLoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }

            var customerAccount = await _context.customers.FirstOrDefaultAsync((customer) => customer.Email == request.Email);

            if (customerAccount == null)
            {
                return null;
            }

            if (new PasswordHasher<Hotel.Domain.Models.Customer>().VerifyHashedPassword(customerAccount, customerAccount.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, customerAccount.Email),
                new Claim("CustomerId", customerAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, customerAccount.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                signingCredentials: cred,
                expires: DateTime.UtcNow.AddMinutes(30)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var httpContext = _contextAccessor.HttpContext;

            if(httpContext != null)
            {
                httpContext.Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Path = "/"
                });
            }

            return new CustomerLoginResponse
            {
                FullName = customerAccount.FullName,
                Email = customerAccount.Email,
            };

        }
    }
}
