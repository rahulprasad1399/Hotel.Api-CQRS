using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<ApiResponse<EmployeeGetAllDto>>
    {
        public int Id { get; set; }
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
