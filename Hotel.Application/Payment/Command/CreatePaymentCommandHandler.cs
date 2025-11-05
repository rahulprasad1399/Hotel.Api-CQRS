using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;

namespace Hotel.Application.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly HotelContext _hotelContext;
        public CreatePaymentCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment payment = new Payment();
            payment.PaymentDate = request.PaymentDate;
            payment.Amount = request.Amount;
            payment.Method = request.Method;
            payment.Status = request.Status;
            payment.BookingId = request.BookingId;

            var newBooking = await _hotelContext.payment.AddAsync(payment);
            var response = await _hotelContext.SaveChangesAsync();

            return newBooking.Entity;
        }
    }
}
