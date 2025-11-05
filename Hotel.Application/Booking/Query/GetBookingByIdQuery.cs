using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.BookingGetById
{
    public class GetBookingByIdQuery : IRequest<Booking>
    {
        public int Id { get; set; }
    }
}
