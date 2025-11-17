using Hotel.Application.BookingGetAll;
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
            Booking booking = await _hotelContext.bookings.Include("Customer").Include("Room").FirstOrDefaultAsync((hotel)=>hotel.Id == request.Id);
            if (booking == null)
            {
                BookingGetAllDto bookingGetAll = new BookingGetAllDto
                {
                    Id = booking.Id,
                    CheckInDate = booking.CheckInDate,
                    CheckOutDate = booking.CheckOutDate,
                    TotalAmount = booking.TotalAmount,
                    Status = booking.Status,
                    CustomerId = booking.CustomerId,
                    CustomerName = booking.Customer.FullName,
                    RoomId = booking.RoomId,
                    RoomNumber = booking.Room.RoomNumber
                };
                return null;
            } else
            {
                return booking;
            }

        }
    }
}
