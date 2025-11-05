using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllHotels
{
    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, List<Domain.Models.Hotel>>
    {
        private readonly HotelContext _context;
        public GetHotelQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Models.Hotel>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _context.hotels.ToListAsync();
            return hotels;
        }
    }
}
