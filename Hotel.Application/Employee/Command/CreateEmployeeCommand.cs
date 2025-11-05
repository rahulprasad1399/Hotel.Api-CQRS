using Hotel.Domain.Models;
using MediatR;

namespace Hotel.Application.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int HotelId { get; set; }
    }
}
