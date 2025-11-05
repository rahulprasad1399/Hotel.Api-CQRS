using Hotel.Infrastructure.Data;
using MediatR;

namespace Hotel.Application.CreateHotels
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, int>
    {
        private readonly HotelContext _context;
        public CreateHotelCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
        {
            Domain.Models.Hotel newHotel = new Domain.Models.Hotel();
            newHotel.Name = command.Name;
            newHotel.Address = command.Address;
            newHotel.City = command.City;
            newHotel.Country = command.Country;
            newHotel.PhoneNumber = command.PhoneNumber;

            await _context.hotels.AddAsync(newHotel);
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
