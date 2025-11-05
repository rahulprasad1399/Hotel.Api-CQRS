using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly HotelContext _hotelContext;
        public DeleteCustomerCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = await _hotelContext.customers.FirstOrDefaultAsync((customer) => customer.Id == request.Id);
            if (customer == null)
            {
                return 0;
            }
            else
            {
                _hotelContext.customers.Remove(customer);
                int res = await _hotelContext.SaveChangesAsync();
                return res;
            }

        }
    }
}
