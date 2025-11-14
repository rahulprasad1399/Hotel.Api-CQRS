using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<Employee>>
    {
        private readonly HotelContext _hotelContext;
        public CreateEmployeeCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<ApiResponse<Employee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var hotel = await _hotelContext.hotels.FirstOrDefaultAsync((hotel) => hotel.Id == request.HotelId);
            if (hotel == null)
            {
                return ApiResponse<Employee>.Fail("Provide a valid Hotel Id");
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
                return ApiResponse<Employee>.Ok(employee);
            }
            else
            {
                return ApiResponse<Employee>.Fail("Failed to save the employee to database");
            }

        }
    }
}
