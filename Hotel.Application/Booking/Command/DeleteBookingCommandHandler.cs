using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, int>
    {
        private readonly HotelContext _hotelcontext;
        public DeleteBookingCommandHandler(HotelContext hotelcontext)
        {
            _hotelcontext = hotelcontext;
        }
        public async Task<int> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            Booking bookingToDelete = await _hotelcontext.bookings.FirstOrDefaultAsync((booking)=> booking.Id == request.Id);
            if (bookingToDelete != null) { 
                _hotelcontext.bookings.Remove(bookingToDelete);
                int response = await _hotelcontext.SaveChangesAsync();
                return response;
            }else
            {
                return 0;
            }
        }
    }
}
