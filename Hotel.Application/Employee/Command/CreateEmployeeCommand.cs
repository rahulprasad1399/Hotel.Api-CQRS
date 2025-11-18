using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<ApiResponse<EmployeeGetAllDto>>
    {
        public string FullName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int HotelId { get; set; }
    }
}
