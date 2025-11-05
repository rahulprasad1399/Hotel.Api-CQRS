using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;

namespace Hotel.Application.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly HotelContext _hotelContext;
        public CreateEmployeeCommandHandler(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = new Employee();
            employee.FullName = request.FullName;
            employee.Role = request.Role;
            employee.Email = request.Email;
            employee.HotelId = request.HotelId;

            var newEmployee = await _hotelContext.employees.AddAsync(employee);
            await _hotelContext.SaveChangesAsync();
            return newEmployee.Entity;
        }
    }
}
