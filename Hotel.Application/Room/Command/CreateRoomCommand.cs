using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.CreateRoom
{
    public class CreateRoomCommand : IRequest<Room>
    {
        public string RoomNumber { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomStatus Status { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
    }
}
