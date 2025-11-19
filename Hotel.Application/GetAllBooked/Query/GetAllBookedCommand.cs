using Hotel.Application.GetAllBookedDto;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllBooked.Query
{
    public  class GetAllBookedCommand : IRequest<RoomBookedDates>
    {
        public int RoomId { get; set; } 
    }

    public class GetAllBookedCommandHandler : IRequestHandler<GetAllBookedCommand, RoomBookedDates>
    {
        private readonly HotelContext _context;
        public GetAllBookedCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<RoomBookedDates> Handle(GetAllBookedCommand request, CancellationToken cancellationToken)
        {
            var selectedRoom = await _context.rooms.Include(x=>x.Bookings).FirstOrDefaultAsync((room)=>room.Id == request.RoomId);

            if (selectedRoom == null) {
                return null;
            }

            return new RoomBookedDates
            {
                RoomNumber = selectedRoom.RoomNumber,
                Status = selectedRoom.Status,
                Bookings = selectedRoom.Bookings.Select(booking => new BookedDateDto
                {
                    checkin = booking.CheckInDate,
                    checkout = booking.CheckOutDate
                }).ToList()
            };
        }
    }
}
