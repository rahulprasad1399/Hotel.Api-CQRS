using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllRoomType
{
    public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, List<RoomType>>
    {
        private readonly HotelContext _context;
        public GetRoomTypeQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<RoomType>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var roomTypes = await _context.roomTypes.ToListAsync();
            return roomTypes;
        }
    }
}
