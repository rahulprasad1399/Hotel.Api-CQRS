using Hotel.Application.RoomGetAll;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllRooms
{
    public class GetAllRoomsQuery : IRequest<List<GetAllRoomDto>>
    {
        public int? hotelId { get; set; }
    }
}
