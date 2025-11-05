

using Hotel.Application.CreatePayment;
using Hotel.Application.DeletePayment;
using Hotel.Application.GetAllPayment;
using Hotel.Application.PaymentGetById;
using Hotel.Application.UpdatePayment;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentCommand command)
        {
            Payment payment = await _mediator.Send(command);
            if (payment != null)
            {
                return Ok(payment);
            }
            return BadRequest(new { message = "Failed to create Customer" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            GetAllPaymentQuery query = new GetAllPaymentQuery();
            List<Payment> payments = await _mediator.Send(query);
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            GetPaymentByIdQuery query = new GetPaymentByIdQuery();
            query.Id = id;
            Payment payment = await _mediator.Send(query);
            if (payment != null)
            {
                return Ok(payment);
            }
            else
            {
                return NotFound(new { message = $"No booking found with id {id}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentCommand command)
        {
            command.Id = id;
            Payment updatedPayment = await _mediator.Send(command);
            if (updatedPayment != null)
            {
                return Ok(updatedPayment);
            }
            else
            {
                return NotFound(new { message = $"Customer not found with the id {id}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            DeletePaymentCommand query = new DeletePaymentCommand();
            query.Id = id;
            int response = await _mediator.Send(query);
            if (response == 1)
            {
                return Ok(new { message = "Successfully deleted booking" });
            }
            else
            {
                return NotFound(new { message = "Failed to delete booking" });
            }
        }
    }
}
