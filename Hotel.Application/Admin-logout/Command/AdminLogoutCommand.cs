using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Admin_logout.Command
{
    public class AdminLogoutCommand : IRequest<string>
    {

    }

    public class AdminLogoutCommandHandler : IRequestHandler<AdminLogoutCommand, string>
    {
        private readonly HotelContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public AdminLogoutCommandHandler(HotelContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public async Task<string> Handle(AdminLogoutCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _contextAccessor.HttpContext;

            if (httpContext == null)
            {
                return null;
            }

            var userId = Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(userId == null)
            {
                return null;
            }

            var employee = await _context.employees.FirstOrDefaultAsync((employee) => employee.Id == userId);

            employee.RefreshToken = null;
            employee.RefreshTokenExpiryTime = null;

            await _context.SaveChangesAsync();

            var deleteOption = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            };

            httpContext.Response.Cookies.Delete("token", deleteOption);
            httpContext.Response.Cookies.Delete("refreshToken", deleteOption);

            return "Logged out successfully";

        }
    }
}
