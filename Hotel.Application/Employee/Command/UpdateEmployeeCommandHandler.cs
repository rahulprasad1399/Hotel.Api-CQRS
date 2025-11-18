using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<EmployeeGetAllDto>>
    {
        private readonly HotelContext _hotelContext;
        public UpdateEmployeeCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<ApiResponse<EmployeeGetAllDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee existingEmployee = await _hotelContext.employees.FirstOrDefaultAsync((employee) => employee.Id == request.Id);

            var hotel = await _hotelContext.hotels.FirstOrDefaultAsync((hotel) => hotel.Id == request.HotelId);
            if (hotel == null)
            {
                return ApiResponse<EmployeeGetAllDto>.Fail("Provide a valid Hotel Id");
            }

            if (existingEmployee != null)
            {
                existingEmployee.FullName = request.FullName;
                existingEmployee.Role = request.Role;
                existingEmployee.Email = request.Email;
                existingEmployee.HotelId = request.HotelId;

                await _hotelContext.SaveChangesAsync();

                EmployeeGetAllDto resEmployee = new EmployeeGetAllDto
                {
                    Id = request.Id,
                    FullName = request.FullName,
                    Email = request.Email,
                    Role = request.Role,
                    HotelId = request.HotelId,
                    HotelName = hotel.Name
                };

                return ApiResponse<EmployeeGetAllDto>.Ok(resEmployee);
            }
            else
            {
                return ApiResponse<EmployeeGetAllDto>.Fail("Provide a valid employee Id");
            }

        }
    }

}
