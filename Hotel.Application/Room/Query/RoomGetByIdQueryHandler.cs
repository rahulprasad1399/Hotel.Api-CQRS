using Hotel.Application.RoomGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetByIdRoom
{
    public class RoomGetByIdQueryHandler : IRequestHandler<RoomGetByIdQuery, GetAllRoomDto>
    {
        private readonly HotelContext _hoteContext;
        public RoomGetByIdQueryHandler(HotelContext hotelContext)
        {
            _hoteContext = hotelContext;   
        }
        public async Task<GetAllRoomDto> Handle(RoomGetByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Room existingRoom = await _hoteContext.rooms.Include("Hotel").Include("RoomType").FirstOrDefaultAsync((room)=>room.Id == id);
            if (existingRoom != null)
            {
                GetAllRoomDto room = new GetAllRoomDto()
                {
                    RoomNumber = existingRoom.RoomNumber,
                    Id = existingRoom.Id,
                    RoomStatus = existingRoom.Status,
                    PricePerNight = existingRoom.PricePerNight,
                    hotelId = existingRoom.HotelId,
                    RoomTypeId = existingRoom.RoomTypeId,
                    HoteName = existingRoom.Hotel.Name,
                    RoomTypeName = existingRoom.RoomType.TypeName
                };
                return room;
            } else
            {
                return null;
            }
        }
    }
}
