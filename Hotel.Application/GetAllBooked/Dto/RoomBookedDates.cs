using Hotel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllBookedDto
{
    public class RoomBookedDates
    {
        public string RoomNumber { get; set; }
        public RoomStatus Status { get; set; }
        public List<BookedDateDto> Bookings { get; set; }
    }
}
