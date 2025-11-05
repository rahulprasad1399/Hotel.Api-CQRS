using Hotel.Application.CreateCustomer;
using Hotel.Application.DeleteCustomer;
using Hotel.Application.GetAllCustomer;
using Hotel.Application.GetCustomer;
using Hotel.Application.UpdateCustomer;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            GetAllCustomerQuery query = new GetAllCustomerQuery();
            List<Customer> customers = await _mediator.Send(query);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            GetCustomerByIdQuery command = new GetCustomerByIdQuery();
            command.Id = id;

            Customer customer = await _mediator.Send(command);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound($"Customer with the id {id} it not found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            int response = await _mediator.Send(command);
            if (response == 1)
            {
                return Ok("Customer Created Successfully");
            }
            return BadRequest("Failed to create Customer");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerCommand command)
        {
            command.Id = id;
            Customer response = await _mediator.Send(command);
            if (response == null)
            {
                return NotFound(new { message = $"Customer with the id {id} not found" });
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand();
            command.Id = id;
            int response = await _mediator.Send(command);
            if (response == 1)
            {
                return Ok(new { message = $"Customer {id} deleted successfully" });
            }
            return BadRequest(new { message = $"Failed to delete Customer" });
        }

    }
}
