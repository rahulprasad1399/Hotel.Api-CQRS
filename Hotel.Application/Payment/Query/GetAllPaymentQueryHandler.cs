using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllPayment
{
    public class GetAllPaymentQueryHandler : IRequestHandler<GetAllPaymentQuery, List<Payment>>
    {
        private readonly HotelContext _hotelContext;
        public GetAllPaymentQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<List<Payment>> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken)
        {
            List<Payment> payments = await _hotelContext.payment.ToListAsync();
            return payments;
        }
    }


}
