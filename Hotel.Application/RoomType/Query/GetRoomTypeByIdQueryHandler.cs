
using Hotel.Application.RoomTypeGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetByIdRoomType
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, RoomTypeGetAllDto>
    {
        private readonly HotelContext _context;
        public GetRoomTypeByIdQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<RoomTypeGetAllDto> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var existingRoomType = await _context.roomTypes.FirstOrDefaultAsync((roomtype) => roomtype.Id == request.Id);
            if (existingRoomType != null)
            {
                RoomTypeGetAllDto roomType = new RoomTypeGetAllDto
                {
                    Id = existingRoomType.Id,
                    TypeName = existingRoomType.TypeName,
                    Description = existingRoomType.Description,
                    Capacity = existingRoomType.Capacity,
                };

                return roomType;
            }
            else
            {
                return null;
            }
        }
    }
}
