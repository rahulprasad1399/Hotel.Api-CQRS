using Hotel.Application.CustomerGetAll;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetCustomer
{
    public class GetCustomerByIdQuery : IRequest<CustomerGetAllDto>
    {
        public int Id { get; set; }
    }
}
