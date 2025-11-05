using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetByIdRoom
{
    public class RoomGetByIdQueryHandler : IRequestHandler<RoomGetByIdQuery, Room>
    {
        private readonly HotelContext _hoteContext;
        public RoomGetByIdQueryHandler(HotelContext hotelContext)
        {
            _hoteContext = hotelContext;   
        }
        public async Task<Room> Handle(RoomGetByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Room existingRoom = await _hoteContext.rooms.FirstOrDefaultAsync((room)=>room.Id == id);
            if (existingRoom != null)
            {
                return existingRoom;
            } else
            {
                return null;
            }
        }
    }
}
