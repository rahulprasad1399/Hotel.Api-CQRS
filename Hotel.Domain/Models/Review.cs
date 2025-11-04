using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
