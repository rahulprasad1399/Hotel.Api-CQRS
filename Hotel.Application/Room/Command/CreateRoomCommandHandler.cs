using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;

namespace Hotel.Application.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Room>
    {
        private readonly HotelContext _hotelContext;
        public CreateRoomCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Room> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            Room room = new Room
            {
                RoomNumber = request.RoomNumber,
                PricePerNight = request.PricePerNight,
                Status = request.Status,
                HotelId = request.HotelId,
                RoomTypeId = request.RoomTypeId
            };

            var newRoom = await _hotelContext.rooms.AddAsync(room);
            await _hotelContext.SaveChangesAsync();
            return newRoom.Entity;
        }
    }
}
