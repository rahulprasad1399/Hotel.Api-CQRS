using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdatePayment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Payment>
    {
        private readonly HotelContext _hotelContext;
        public UpdatePaymentCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Payment> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment existingPayment = await _hotelContext.payment.FirstOrDefaultAsync((payment) => payment.Id == request.Id);
            if (existingPayment != null)
            {
                existingPayment.PaymentDate = request.PaymentDate;
                existingPayment.Amount = request.Amount;
                existingPayment.Method = request.Method;
                existingPayment.Status = request.Status;
                existingPayment.BookingId = request.BookingId;

                await _hotelContext.SaveChangesAsync();
                return existingPayment;
            }
            return null;
        }
    }
}
