using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeGetAllDto>>
    {
        private readonly HotelContext _hotelContext;
        public CreateEmployeeCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<ApiResponse<EmployeeGetAllDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var hotel = await _hotelContext.hotels.FirstOrDefaultAsync((hotel) => hotel.Id == request.HotelId);
            if (hotel == null)
            {
                return ApiResponse<EmployeeGetAllDto>.Fail("Provide a valid Hotel Id");
            }

            Employee employee = new Employee();
            employee.FullName = request.FullName;
            employee.Role = request.Role;
            employee.Email = request.Email;
            employee.HotelId = request.HotelId;

            var newEmployee = await _hotelContext.employees.AddAsync(employee);
            int aaffectedRows = await _hotelContext.SaveChangesAsync();
            if (aaffectedRows > 0)
            {
                var emp = newEmployee.Entity;
                EmployeeGetAllDto resEmployee = new EmployeeGetAllDto
                {
                    Id = emp.Id,
                    FullName = emp.FullName,
                    Email = emp.Email,
                    Role = emp.Role,
                    HotelId = emp.HotelId,
                    HotelName = emp.Hotel.Name
                };
                return ApiResponse<EmployeeGetAllDto>.Ok(resEmployee);
            }
            else
            {
                return ApiResponse<EmployeeGetAllDto>.Fail("Failed to save the employee to database");
            }

        }
    }
}

