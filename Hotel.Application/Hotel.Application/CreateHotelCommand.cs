using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Hotels
{
    public class CreateHotelCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, int>
    {
        private readonly HotelContext _context;
        public CreateHotelCommandHandler(HotelContext context)
        {
            this._context = context;
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
            var result =  await _context.SaveChangesAsync();

            return result;
        }
    }
}
