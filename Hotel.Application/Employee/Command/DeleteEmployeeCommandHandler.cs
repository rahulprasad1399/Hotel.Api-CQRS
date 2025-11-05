using Hotel.Domain.Models;
using Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly HotelContext _hotelcontext;
        public DeleteEmployeeCommandHandler(HotelContext hotelcontext)
        {
            _hotelcontext = hotelcontext;
        }
        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employeeToDelete = await _hotelcontext.employees.FirstOrDefaultAsync((employee) => employee.Id == request.Id);
            if (employeeToDelete != null)
            {
                _hotelcontext.employees.Remove(employeeToDelete);
                int response = await _hotelcontext.SaveChangesAsync();
                return response;
            }
            else
            {
                return 0;
            }
        }
    }
}
