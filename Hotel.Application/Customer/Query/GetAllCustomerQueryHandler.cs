using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<Customer>>
    {
        private readonly HotelContext _context;
        public GetAllCustomerQueryHandler(HotelContext context)
        {
            this._context = context;
        }

        public async Task<List<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            List<Customer> customers = await _context.customers.ToListAsync();
            return customers;
        }
    }
}
