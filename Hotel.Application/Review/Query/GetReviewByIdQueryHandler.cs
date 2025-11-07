using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Review>
    {
        private readonly HotelContext _context;
        public GetReviewByIdQueryHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<Review> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Review review = await _context.reviews.FirstOrDefaultAsync((review) => review.Id == id);
            if (review != null)
            {
                return review;
            }
            else
            {
                return null;
            }
        }
    }
}
