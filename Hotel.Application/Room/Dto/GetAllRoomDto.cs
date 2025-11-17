using Hotel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.RoomGetAll
{
    public class GetAllRoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public RoomStatus RoomStatus { get; set; }
        public decimal PricePerNight { get; set; }
        public int hotelId { get; set; }
        public string HoteName { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
    }
}
