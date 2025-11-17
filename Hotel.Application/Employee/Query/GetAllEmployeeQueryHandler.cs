using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.GetAllEmployee
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<EmployeeGetAllDto>>
    {
        private readonly HotelContext _hotelContext;
        public GetAllEmployeeQueryHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<List<EmployeeGetAllDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employees = await _hotelContext.employees.Include("Hotel").ToListAsync();
            List<EmployeeGetAllDto> employeeGetAllDto = employees.Select((employee) => new EmployeeGetAllDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                HotelId = employee.HotelId,
                HotelName = employee.Hotel?.Name ?? "No Hotel Assigned",
                Role = employee.Role
            }).ToList();

            return employeeGetAllDto;
        }
    }
}


