
using Hotel.Application.CreateHotels;
using Hotel.Application.DeleteHotels;
using Hotel.Application.GetAllHotels;
using Hotel.Application.GetByIdHotels;
using Hotel.Application.UpdateHotels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HotelController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int response = await _mediator.Send(command);
            if (response == 1)
            {
                return Ok(new { message = "Hotel added Successfully" });
            }
            else
            {
                return BadRequest("Failed to add Hotel");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels([FromQuery]string? destination, [FromQuery] DateTime? checkin, [FromQuery] DateTime? checkout, [FromQuery] int? price )
        {
            GetHotelQuery query = new GetHotelQuery();
            query.destination = destination;
            query.checkin = checkin;
            query.checkout = checkout;
            query.price = price;
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            GetByIdHotelQuery query = new GetByIdHotelQuery();
            query.Id = id;
            var response = await _mediator.Send(query);
            if (response == null)
            {
                return BadRequest($"Customer not found with id {id}");
            }
            else
            {
                return Ok(response);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            command.Id = id;

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            DeleteHotelCommand command = new DeleteHotelCommand();
            command.id = id;
            var response = await _mediator.Send(command);
            if(response == 2)
            {
                return BadRequest($"Room Type with id {id} dosen't exist");
            } else if(response == 1)
            {
                return Ok(new { message = "Hotel Deleted Successfully" }); 
            }
            else
            {
                return BadRequest("Failed to save to database");
            }
        }
    }
}
