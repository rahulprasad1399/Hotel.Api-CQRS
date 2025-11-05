using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;

namespace Hotel.Application.CreateBooking
{
    public class CreateBookingCommandCommandHandler : IRequestHandler<CreateBookingCommand, Booking>
    {
        private readonly HotelContext _hotelContext;
        public CreateBookingCommandCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            Booking booking = new Booking();
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            booking.TotalAmount = request.TotalAmount;
            booking.Status = request.Status;
            booking.CustomerId = request.CustomerId;
            booking.RoomId = request.RoomId;

            var newBooking = await _hotelContext.bookings.AddAsync(booking);
            var response = await _hotelContext.SaveChangesAsync();

            return newBooking.Entity;
        }

    }
}
