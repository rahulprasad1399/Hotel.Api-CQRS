using Hotel.Application.CustomerGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetCustomer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerGetAllDto>
    {
        private readonly HotelContext _context;
        public GetCustomerByIdQueryHandler(HotelContext context)
        {
            this._context = context;
        }
        public async Task<CustomerGetAllDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            Customer customerFound = await _context.customers.FirstOrDefaultAsync((customer) => customer.Id == id);
            if (customerFound != null)
            {
                CustomerGetAllDto customer = new CustomerGetAllDto
                {
                    Id = customerFound.Id,
                    FullName = customerFound.FullName,
                    Email = customerFound.Email,
                    PhoneNumber = customerFound.PhoneNumber,
                    IdProofNumber = customerFound.IdProofNumber,
                };

                return customer;
            }
            else
            {
                return null;
            }
        }
    }
}
