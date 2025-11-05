using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllRoomType
{
    public class GetRoomTypeQuery : IRequest<List<RoomType>>
    {

    }
}
