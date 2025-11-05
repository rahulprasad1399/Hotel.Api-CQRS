using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<Room>>
    {
        private readonly HotelContext _hotelContext;
        public GetAllRoomsQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<List<Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            List<Room> rooms = await _hotelContext.rooms.ToListAsync();
            return rooms;
        }
    }
}
