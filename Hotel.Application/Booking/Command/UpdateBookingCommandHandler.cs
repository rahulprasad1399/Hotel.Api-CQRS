using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Booking>
    {
        private readonly HotelContext _hotelContext;
        public UpdateBookingCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Booking> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            Booking existingBooking = await _hotelContext.bookings.FirstOrDefaultAsync((booking)=>booking.Id == request.Id);
            if(existingBooking != null)
            {
                existingBooking.CheckInDate = request.CheckInDate;
                existingBooking.CheckOutDate = request.CheckOutDate;
                existingBooking.TotalAmount = request.TotalAmount;
                existingBooking.Status = request.Status;
                existingBooking.CustomerId = request.CustomerId;
                existingBooking.RoomId = request.RoomId;

                await _hotelContext.SaveChangesAsync();
                return existingBooking;
            }
            return null;
        }
    }
}
