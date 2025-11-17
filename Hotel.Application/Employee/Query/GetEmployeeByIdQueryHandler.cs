using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeGetAllDto>
    {
        private readonly HotelContext _hotelContext;
        public GetEmployeeByIdQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<EmployeeGetAllDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _hotelContext.employees.Include("Hotel").FirstOrDefaultAsync((hotel) => hotel.Id == request.Id);
            if (employee != null)
            {
                EmployeeGetAllDto employeeGetAllDto = new EmployeeGetAllDto
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Email = employee.Email,
                    HotelId = employee.Hotel.Id,
                    HotelName = employee.Hotel.Name,
                    Role = employee.Role
                };
                return employeeGetAllDto;
            }
            else
            {
                return null;
            }
        }
    }
}
