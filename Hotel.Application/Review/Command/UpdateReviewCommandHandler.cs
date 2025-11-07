using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Review>
    {
        private readonly HotelContext _context;
        public UpdateReviewCommandHandler(HotelContext context)
        {
            _context = context;
        }

        public async Task<Review> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Review foundReview = await _context.reviews.FirstOrDefaultAsync((review) => review.Id == id);
            if (foundReview != null)
            {
                foundReview.Rating = request.Rating;
                foundReview.Comment = request.Comment;
                foundReview.ReviewDate = request.ReviewDate;
                foundReview.HotelId = request.HotelId;
                foundReview.CustomerId = request.CustomerId;

                await _context.SaveChangesAsync();

                return foundReview;
            }
            else
            {
                return null;
            }
        }
    }
}
