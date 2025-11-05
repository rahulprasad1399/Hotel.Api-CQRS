using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, int>
    {
        private readonly HotelContext _context;
        public DeleteRoomTypeCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteRoomTypeCommand command, CancellationToken cancellationToken)
        {
            int id = command.Id;
            var roomTypeToDelete = await _context.roomTypes.FirstOrDefaultAsync((roomType) => roomType.Id == id);

            if (roomTypeToDelete != null)
            {
                _context.roomTypes.Remove(roomTypeToDelete);
                int response = await _context.SaveChangesAsync();
                return response;
            }
            else
            {
                return 0;
            }
        }
    }
}
