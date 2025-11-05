using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.DeleteRoomType
{
    public class DeleteRoomTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
