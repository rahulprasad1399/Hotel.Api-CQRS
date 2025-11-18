using Azure;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UpdateRoom
{
    public class UpdateRoomCommand : IRequest<Room>
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomStatus Status { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public string Image {  get; set; }
    }
}
