using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllBookings
{
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingtQuery, List<Booking>>
    {
        private readonly HotelContext _hotelContext;
        public GetAllBookingQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<List<Booking>> Handle(GetAllBookingtQuery request, CancellationToken cancellationToken)
        {
            List<Booking> bookings = await _hotelContext.bookings.ToListAsync();
            return bookings;
        }
    }


}
