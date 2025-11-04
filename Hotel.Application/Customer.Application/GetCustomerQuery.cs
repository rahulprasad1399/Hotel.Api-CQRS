using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.Customers
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
    {
        private readonly HotelContext _context;
        public GetCustomerQueryHandler(HotelContext context)
        {
            this._context = context;
        }

        public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.customers.ToListAsync();
            return customers;
        }
    }
}

