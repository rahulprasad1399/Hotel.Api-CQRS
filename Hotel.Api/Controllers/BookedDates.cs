using Hotel.Application.GetAllBooked.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookedDates : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookedDates(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookingDates([FromQuery] int RoomId)
        {
            GetAllBookedCommand command = new GetAllBookedCommand();
            command.RoomId = RoomId;
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
