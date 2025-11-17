

using Hotel.Application.CreateRoomType;
using Hotel.Application.DeleteRoomType;
using Hotel.Application.GetAllRoomType;
using Hotel.Application.GetByIdRoomType;
using Hotel.Application.UpdateRoomType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Hotel.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomTypeController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand command)
        {
            int response = await _mediator.Send(command);
            if (response == 1)
            {
                return Ok(new { message = "Room Type Created Successfully" });
            }
            else
            {
                return BadRequest("Failed to create a Room Type");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomTypes()
        {
            GetRoomTypeQuery query = new GetRoomTypeQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomType(int id)
        {
            GetRoomTypeByIdQuery query = new GetRoomTypeByIdQuery();
            query.Id = id;

            var response = await _mediator.Send(query);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomType(int id, UpdateRoomTypeCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            DeleteRoomTypeCommand command = new DeleteRoomTypeCommand();
            command.Id = id;

            var response = await _mediator.Send(command);
            if (response == 1)
            {
                return Ok(new { message = "Room Type Deleted Successfully" });
            }
            return BadRequest();
        }
    }
}