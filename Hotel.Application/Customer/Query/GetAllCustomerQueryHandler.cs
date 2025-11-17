using Hotel.Application.CustomerGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerGetAllDto>>
    {
        private readonly HotelContext _context;
        public GetAllCustomerQueryHandler(HotelContext context)
        {
            this._context = context;
        }

        public async Task<List<CustomerGetAllDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            List<Customer> customers = await _context.customers.ToListAsync();
            List<CustomerGetAllDto> customerGetAll = customers.Select(x => new CustomerGetAllDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IdProofNumber = x.IdProofNumber,
            }).ToList();
            return customerGetAll;
        }
    }
}
