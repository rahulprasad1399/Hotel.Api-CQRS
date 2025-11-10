using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Application.UpdateReview
{
    public class UpdateReviewCommand : IRequest<Review>
    {
        [JsonIgnore]
        public int Id { get; set; }
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
