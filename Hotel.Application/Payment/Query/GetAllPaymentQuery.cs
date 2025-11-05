using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllPayment
{
    public class GetAllPaymentQuery : IRequest<List<Payment>>
    {

    }


}
