using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetByIdRoomType
{
    public class GetRoomTypeByIdQuery : IRequest<RoomType>
    {
        public int Id { get; set; }
    }
}
