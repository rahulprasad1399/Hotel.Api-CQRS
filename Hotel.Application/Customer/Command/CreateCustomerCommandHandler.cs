using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using System.Runtime.CompilerServices;

namespace Hotel.Application.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly HotelContext _context;
        public CreateCustomerCommandHandler(HotelContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = new Customer
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IdProofNumber = request.IdProofNumber
            };

            await _context.customers.AddAsync(customer);
            int res = await _context.SaveChangesAsync();
            return res;
        }
    }
}
