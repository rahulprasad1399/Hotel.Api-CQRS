using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly HotelContext _hotelContext;
        public UpdateEmployeeCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee existingEmployee = await _hotelContext.employees.FirstOrDefaultAsync((employee) => employee.Id == request.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FullName = request.FullName;
                existingEmployee.Role = request.Role;
                existingEmployee.Email = request.Email;
                existingEmployee.HotelId = request.HotelId;

                await _hotelContext.SaveChangesAsync();
                return existingEmployee;
            }
            return null;
        }
    }

}
