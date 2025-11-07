using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllReviews
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<Review>>
    {
        private readonly HotelContext _context;
        public GetAllEmployeeQueryHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<List<Review>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            List<Review> reviews = await _context.reviews.ToListAsync();
            return reviews;
        }
    }
}
