
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetByIdRoomType
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, RoomType>
    {
        private readonly HotelContext _context;
        public GetRoomTypeByIdQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<RoomType> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var existingRoomType = await _context.roomTypes.FirstOrDefaultAsync((roomtype) => roomtype.Id == request.Id);
            if (existingRoomType != null)
            {
                return existingRoomType;
            }
            else
            {
                return null;
            }
        }
    }
}
