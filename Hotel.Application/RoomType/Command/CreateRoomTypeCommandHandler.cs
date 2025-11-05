using Hotel.Infrastructure.Data;
using MediatR;

namespace Hotel.Application.CreateRoomType
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, int>
    {
        private readonly HotelContext _context;
        public CreateRoomTypeCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            Domain.Models.RoomType roomType = new Domain.Models.RoomType();
            roomType.TypeName = command.TypeName;
            roomType.Description = command.Description;
            roomType.Capacity = command.Capacity.Value;

            await _context.roomTypes.AddAsync(roomType);
            int reponse = await _context.SaveChangesAsync();
            return reponse;
        }
    }
}
