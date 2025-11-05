using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetCustomer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly HotelContext _context;
        public GetCustomerByIdQueryHandler(HotelContext context)
        {
            this._context = context;
        }
        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Customer customerFound = await _context.customers.FirstOrDefaultAsync((customer)=>customer.Id == id);
            if (customerFound != null)
            {
                return customerFound;
            }
            else
            {
                return null;
            }
        }
    }
}
