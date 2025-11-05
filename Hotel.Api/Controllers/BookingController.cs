using Hotel.Application.BookingGetById;
using Hotel.Application.CreateBooking;
using Hotel.Application.DeleteBooking;
using Hotel.Application.GetAllBookings;
using Hotel.Application.UpdateBooking;
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

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            GetAllBookingtQuery query = new GetAllBookingtQuery();
            List<Booking> bookings = await _mediator.Send(query);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            GetBookingByIdQuery query = new GetBookingByIdQuery();
            query.Id = id;
            Booking booking = await _mediator.Send(query);
            if(booking != null)
            {
                return Ok(booking);
            } else
            {
                return NotFound(new {message = $"No booking found with id {id}"});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, UpdateBookingCommand command)
        {
            command.Id = id;
            Booking updatedBooking = await _mediator.Send(command);
            if (updatedBooking != null)
            {
                return Ok(updatedBooking);
            } else
            {
                return NotFound(new { message = $"Customer not found with the id {id}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            DeleteBookingCommand query = new DeleteBookingCommand();
            query.Id = id;  
            int response = await _mediator.Send(query);
            if (response == 1) {
                return Ok(new {message = "Successfully deleted booking"});
            } else
            {
                return NotFound(new { message = "Failed to delete booking" });
            }
        }
    }
}
