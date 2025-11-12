using Hotel.Application.HotelDto;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllHotels
{
    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, List<HotelGetDto>>
    {
        private readonly HotelContext _context;
        public GetHotelQueryHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<HotelGetDto>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var query = _context.hotels.Include(r => r.Rooms).ThenInclude(h => h.Bookings).AsQueryable();

            if (!string.IsNullOrEmpty(request.destination))
            {
                string destination = request.destination.ToLower();
                query = query.Where((hotel) => hotel.City.ToLower().Contains(destination) || hotel.Country.ToLower().Contains(destination));
            }

            if (request.checkin.HasValue && request.checkout.HasValue)
            {
                var checkin = request.checkin.Value;
                var checkout = request.checkout.Value;

                query = query.Where((hotel) => hotel.Rooms.Any((room) => room.Bookings.All((booking) => checkin > booking.CheckOutDate || checkout < booking.CheckInDate)));
            }

            List<Hotel.Domain.Models.Hotel> hotels = await query.ToListAsync();


            var hotellist = hotels.Select((hotel) => new HotelGetDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                PhoneNumber = hotel.PhoneNumber,
            }).ToList();

            return hotellist;

        }
    }
}
