using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllReviews
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, List<Review>>
    {
        private readonly HotelContext _context;
        public GetAllReviewQueryHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<List<Review>> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            List<Review> reviews = await _context.reviews.ToListAsync();
            return reviews;
        }
    }
}
