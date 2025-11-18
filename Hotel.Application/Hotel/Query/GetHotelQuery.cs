using Hotel.Application.HotelDto;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllHotels
{
    public class GetHotelQuery : IRequest<List<HotelGetDto>>
    {
        public string? destination { get; set; }
        public DateTime? checkin { get; set; }
        public DateTime? checkout { get; set; }
        public int? price { get; set; }
    }
}
