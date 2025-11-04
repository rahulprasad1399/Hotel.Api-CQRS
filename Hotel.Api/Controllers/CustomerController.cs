using Hotel.Application.Customers;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Customer>> GetAllCustomer()
        {
            GetCustomersQuery query = new GetCustomersQuery();  
            return await mediator.Send(query);
        }
    }
}
