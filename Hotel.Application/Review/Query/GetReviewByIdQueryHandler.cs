using Hotel.Application.ReviewGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewGetAllDto>
    {
        private readonly HotelContext _context;
        public GetReviewByIdQueryHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<ReviewGetAllDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Review review = await _context.reviews.FirstOrDefaultAsync((review) => review.Id == id);
            if (review != null)
            {
                ReviewGetAllDto reviewGet = new ReviewGetAllDto
                {
                    Id = review.Id,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    ReviewDate = review.ReviewDate,
                    HotelId = review.HotelId,
                    HotelName = review.Hotel.Name,
                    CustomerId = review.CustomerId,
                    CustomerName = review.Customer.FullName
                };
                return reviewGet;
            }
            else
            {
                return null;
            }
        }
    }
}