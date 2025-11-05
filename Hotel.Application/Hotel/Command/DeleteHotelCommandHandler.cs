using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteHotels
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, int>
    {
        private readonly HotelContext _context;
        public DeleteHotelCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteHotelCommand command, CancellationToken cancellationToken)
        {
            int id = command.id;
            var hotelToDelete = await _context.hotels.FirstOrDefaultAsync((hotel) => hotel.Id == id);
            if (hotelToDelete == null)
            {
                return 2;
            }
            else
            {
                _context.hotels.Remove(hotelToDelete);
                var res = await _context.SaveChangesAsync();
                return res;
            }
        }
    }
}
