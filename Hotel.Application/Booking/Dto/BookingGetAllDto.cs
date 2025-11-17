using Hotel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.BookingGetAll
{
    public class BookingGetAllDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
    }
}
