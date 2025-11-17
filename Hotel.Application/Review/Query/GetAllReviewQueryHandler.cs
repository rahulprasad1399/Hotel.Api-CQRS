using Hotel.Application.ReviewGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllReviews
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, List<ReviewGetAllDto>>
    {
        private readonly HotelContext _context;
        public GetAllReviewQueryHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<List<ReviewGetAllDto>> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            List<Review> reviews = await _context.reviews.Include("Hotel").Include("Customer").ToListAsync();
            List<ReviewGetAllDto> reviewsGetAll = reviews.Select((review) => new ReviewGetAllDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                HotelId = review.HotelId,
                HotelName = review.Hotel.Name,
                CustomerId = review.CustomerId,
                CustomerName = review.Customer.FullName
            }).ToList();
            return reviewsGetAll;
        }
    }
}
