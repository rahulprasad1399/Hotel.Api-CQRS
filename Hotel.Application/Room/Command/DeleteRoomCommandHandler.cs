using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, int>
    {
        private readonly HotelContext _context;
        public DeleteRoomCommandHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            Room roomToDelete = await _context.rooms.FirstOrDefaultAsync((room)=>room.Id == request.Id);
            if (roomToDelete != null) { 
                _context.rooms.Remove(roomToDelete);
                int response = await _context.SaveChangesAsync();
                return response;
            }
            return 0;
        }
    }
}
