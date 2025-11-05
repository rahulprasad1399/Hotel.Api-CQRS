using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly HotelContext _context;
        public UpdateCustomerCommandHandler(HotelContext context)
        {
            this._context = context;
        }
        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Customer existingCustomer = await _context.customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer != null)
            {
                existingCustomer.FullName = request.FullName;
                existingCustomer.Email = request.Email;
                existingCustomer.PhoneNumber = request.PhoneNumber;
                existingCustomer.IdProofNumber = request.IdProofNumber;

                await _context.SaveChangesAsync();
                return existingCustomer;
            }
            else
            {
                return null;
            }
        }
    }
}
