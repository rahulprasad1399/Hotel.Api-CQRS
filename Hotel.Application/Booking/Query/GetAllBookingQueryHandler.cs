using Hotel.Application.BookingGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllBookings
{
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingtQuery, List<BookingGetAllDto>>
    {
        private readonly HotelContext _hotelContext;
        public GetAllBookingQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<List<BookingGetAllDto>> Handle(GetAllBookingtQuery request, CancellationToken cancellationToken)
        {
            List<Booking> bookings = await _hotelContext.bookings.Include("Customer").Include("Room").ToListAsync();
            List<BookingGetAllDto> bookingGetAll = bookings.Select(x => new BookingGetAllDto
            {
                Id = x.Id,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.FullName,
                RoomId = x.RoomId,
                RoomNumber = x.Room.RoomNumber
            }).ToList();
            return bookingGetAll;
        }
    }


}
