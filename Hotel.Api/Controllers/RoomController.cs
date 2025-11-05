using Hotel.Application.CreateRoom;
using Hotel.Application.DeleteRoom;
using Hotel.Application.GetAllRooms;
using Hotel.Application.GetByIdRoom;
using Hotel.Application.UpdateRoom;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }
                  
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            GetAllRoomsQuery query = new GetAllRoomsQuery();
            List<Room> rooms = await _mediator.Send(query);
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand command)
        {
            Room room = await _mediator.Send(command);
            if (room != null)
            {
                return Ok(room);
            }
            else
            {
                return BadRequest(new { message = "Failed to create a customer" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            RoomGetByIdQuery roomGetByIdQuery = new RoomGetByIdQuery();
            roomGetByIdQuery.Id = id;

            Room roomFound = await _mediator.Send(roomGetByIdQuery);
            if (roomFound != null)
            {
                return Ok(roomFound);   
            } else
            {
                return NotFound(new {message = $"Room not found with id {id}"});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            if (response != null)
            {
                return Ok(response);
            } else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            DeleteRoomCommand query = new DeleteRoomCommand();
            query.Id = id;
            int res = await _mediator.Send(query);
            if(res == 1)
            {
                return Ok(new { message = "Room delete successfully" });
            }
            return BadRequest(new { message = "Failed to delete Room" });
        }
    }
}
