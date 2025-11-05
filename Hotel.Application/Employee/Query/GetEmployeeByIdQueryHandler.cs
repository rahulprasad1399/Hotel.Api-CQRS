using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly HotelContext _hotelContext;
        public GetEmployeeByIdQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _hotelContext.employees.FirstOrDefaultAsync((hotel) => hotel.Id == request.Id);
            if (employee == null)
            {
                return null;
            }
            else
            {
                return employee;
            }
        }
    }
}
