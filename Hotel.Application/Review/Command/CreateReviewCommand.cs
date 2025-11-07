using Hotel.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace Hotel.Application.CreateReview
{
    public class CreateReviewCommand : IRequest<Review>
    {
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
