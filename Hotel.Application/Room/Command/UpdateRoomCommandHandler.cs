using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Room>
    {
        private readonly HotelContext _context;
        public UpdateRoomCommandHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<Room> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Room roomToUpdate = await _context.rooms.FirstOrDefaultAsync((room)=>room.Id == id);
            if (roomToUpdate != null) {
                roomToUpdate.RoomNumber = request.RoomNumber;
                roomToUpdate.PricePerNight = request.PricePerNight;
                roomToUpdate.Status = request.Status;
                roomToUpdate.HotelId = request.HotelId;
                roomToUpdate.RoomTypeId = request.RoomTypeId;
                roomToUpdate.Image = request.Image;

                var response = await _context.SaveChangesAsync();
                if(response == 1)
                {
                    return roomToUpdate;
                }else
                {
                    return null;
                }
                

            } else
            {
                return null;
            }
            
        }
    }
}
