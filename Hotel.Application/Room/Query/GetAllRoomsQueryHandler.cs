using Hotel.Application.RoomGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<GetAllRoomDto>>
    {
        private readonly HotelContext _hotelContext;
        public GetAllRoomsQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<List<GetAllRoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var query = _hotelContext.rooms.Include("Hotel").Include("RoomType").AsQueryable();

            if (request.hotelId.HasValue)
            {
                query = query.Where((room) => room.HotelId == request.hotelId.Value);
            }

            var rooms = await query.ToListAsync();

            List<GetAllRoomDto> getAllRooms = rooms.Select((room) => new GetAllRoomDto
            {
                RoomNumber = room.RoomNumber,
                Id = room.Id,
                RoomStatus = room.Status,
                PricePerNight = room.PricePerNight,
                hotelId = room.HotelId,
                RoomTypeId = room.RoomTypeId,
                HoteName = room.Hotel.Name,
                RoomTypeName = room.RoomType.TypeName,
                Image = room.Image
            }).ToList();

            return getAllRooms;
        }
    }
}
