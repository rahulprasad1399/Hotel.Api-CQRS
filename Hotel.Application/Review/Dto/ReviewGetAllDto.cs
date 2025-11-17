using Hotel.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.ReviewGetAll
{
    public class ReviewGetAllDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
