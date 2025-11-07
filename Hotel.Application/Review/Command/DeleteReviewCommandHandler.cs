using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
    {
        private readonly HotelContext _context;
        public DeleteReviewCommandHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review review = await _context.reviews.FirstOrDefaultAsync((review) => review.Id == request.Id);
            if (review != null)
            {
                _context.reviews.Remove(review);
                int res = await _context.SaveChangesAsync();
                return res;
            }

            return 0;
        }
    }
}
