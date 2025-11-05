using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.BookingGetById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, Booking>
    {
        private readonly HotelContext _hotelContext;
        public GetBookingByIdQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Booking> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            Booking booking = await _hotelContext.bookings.FirstOrDefaultAsync((hotel)=>hotel.Id == request.Id);
            if (booking == null)
            {
                return null;
            } else
            {
                return booking;
            }

        }
    }
}
