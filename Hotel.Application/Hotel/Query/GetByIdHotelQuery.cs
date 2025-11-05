using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetByIdHotels
{
    public class GetByIdHotelQuery : IRequest<Hotel.Domain.Models.Hotel>
    {
        public int Id { get; set; }
    }
}
