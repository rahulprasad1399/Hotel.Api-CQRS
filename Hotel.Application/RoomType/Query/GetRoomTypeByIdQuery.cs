using Hotel.Application.RoomTypeGetAll;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetByIdRoomType
{
    public class GetRoomTypeByIdQuery : IRequest<RoomTypeGetAllDto>
    {
        public int Id { get; set; }
    }
}
