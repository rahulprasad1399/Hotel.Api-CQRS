using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Hotel.Application.CreateBooking
{
    public class CreateBookingCommandCommandHandler : IRequestHandler<CreateBookingCommand, Booking>
    {
        private readonly HotelContext _hotelContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateBookingCommandCommandHandler(HotelContext hotelContext, IHttpContextAccessor httpContextAccessor)
        {
            _hotelContext = hotelContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {

            var token = _httpContextAccessor.HttpContext?
                .Request
                .Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var CustomerIdClaim = jwtToken.Claims.FirstOrDefault(c=>c.Type == "CustomerId")?.Value;

            if (!int.TryParse(CustomerIdClaim, out int customerId))
            {
                return null;
            }

            Booking booking = new Booking();
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            booking.TotalAmount = request.TotalAmount;
            booking.Status = request.Status;
            booking.CustomerId = customerId;
            booking.RoomId = request.RoomId;

            var newBooking = await _hotelContext.bookings.AddAsync(booking);
            var response = await _hotelContext.SaveChangesAsync();

            return newBooking.Entity;
        }

    }
}
