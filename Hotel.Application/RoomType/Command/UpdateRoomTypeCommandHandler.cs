using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateRoomType
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, RoomType>
    {
        private readonly HotelContext _context;
        public UpdateRoomTypeCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<RoomType> Handle(UpdateRoomTypeCommand command, CancellationToken cancellationToken)
        {

            var roomTypeToUpdate = await _context.roomTypes.FirstOrDefaultAsync((roomType) => roomType.Id == command.Id);
            roomTypeToUpdate.TypeName = command.TypeName;
            roomTypeToUpdate.Description = command.Description;
            roomTypeToUpdate.Capacity = command.Capacity.Value;

            await _context.SaveChangesAsync();
            return roomTypeToUpdate;
        }
    }
}
