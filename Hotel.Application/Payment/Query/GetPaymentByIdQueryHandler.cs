using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.PaymentGetById
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Payment>
    {
        private readonly HotelContext _hotelContext;
        public GetPaymentByIdQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            Payment payment = await _hotelContext.payment.FirstOrDefaultAsync((hotel)=>hotel.Id == request.Id);
            if (payment == null)
            {
                return null;
            } else
            {
                return payment;
            }

        }
    }
}
