using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeletePayment
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, int>
    {
        private readonly HotelContext _hotelcontext;
        public DeletePaymentCommandHandler(HotelContext hotelcontext)
        {
            _hotelcontext = hotelcontext;
        }
        public async Task<int> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment paymentToDelete = await _hotelcontext.payment.FirstOrDefaultAsync((payment) => payment.Id == request.Id);
            if (paymentToDelete != null) { 
                _hotelcontext.payment.Remove(paymentToDelete);
                int response = await _hotelcontext.SaveChangesAsync();
                return response;
            }else
            {
                return 0;
            }
        }
    }
}
