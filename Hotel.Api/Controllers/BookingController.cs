using Hotel.Application.CreateBooking;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingCommand command)
        {
            Booking booking = await _mediator.Send(command);
            if(booking != null)
            {
               return Ok(booking);
            }
            return BadRequest(new { message = "Failed to create Customer" });
        }
    }
}
