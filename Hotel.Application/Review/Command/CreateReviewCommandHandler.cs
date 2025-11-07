using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;


namespace Hotel.Application.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Review>
    {
        private readonly HotelContext _context;
        public CreateReviewCommandHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            Review review = new Review();
            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.ReviewDate = request.ReviewDate;
            review.CustomerId = request.CustomerId; 
            review.HotelId = request.HotelId;

            var newReview = _context.reviews.Add(review);
            await _context.SaveChangesAsync();
            return newReview.Entity;
        }
    }
}
