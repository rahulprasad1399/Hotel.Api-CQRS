using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetByIdHotels
{
    public class GetByIdHotelQueryHandler : IRequestHandler<GetByIdHotelQuery, Hotel.Domain.Models.Hotel>
    {
        private readonly HotelContext _context;
        public GetByIdHotelQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<Hotel.Domain.Models.Hotel> Handle(GetByIdHotelQuery request, CancellationToken cancellationToken)
        {
            Hotel.Domain.Models.Hotel hotel = await _context.hotels.FirstOrDefaultAsync((customer) => customer.Id == request.Id);
            return hotel;
        }
    }
}
