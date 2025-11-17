using Hotel.Application.RoomTypeGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllRoomType
{
    public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, List<RoomTypeGetAllDto>>
    {
        private readonly HotelContext _context;
        public GetRoomTypeQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<RoomTypeGetAllDto>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var roomTypes = await _context.roomTypes.ToListAsync();

            List<RoomTypeGetAllDto> getAllRoomType = roomTypes.Select((roomtype) => new RoomTypeGetAllDto
            {
                Id = roomtype.Id,
                TypeName = roomtype.TypeName,
                Description = roomtype.Description,
                Capacity = roomtype.Capacity,
            }).ToList();

            return getAllRoomType;
        }
    }
}
