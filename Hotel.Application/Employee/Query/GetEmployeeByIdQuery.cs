using Hotel.Application.EmployeeGetAll;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeGetAllDto>
    {
        public int Id { get; set; }
    }
}
