using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateHotels
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Hotel.Domain.Models.Hotel>
    {
        private readonly HotelContext _context;
        public UpdateHotelCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<Hotel.Domain.Models.Hotel> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            int id = command.Id;

            var existingHotel = await _context.hotels.FirstOrDefaultAsync((hotel) => hotel.Id == id);

            existingHotel.Address = command.Address;
            existingHotel.City = command.City;
            existingHotel.Country = command.Country;
            existingHotel.PhoneNumber = command.PhoneNumber;
            existingHotel.Name = command.Name;

            await _context.SaveChangesAsync();
            return existingHotel;
        }
    }
}

