

using Hotel.Application.CreateEmployee;
using Hotel.Application.DeleteEmployee;
using Hotel.Application.GetAllEmployee;
using Hotel.Application.GetAllEmployeeById;
using Hotel.Application.LoginRequest;
using Hotel.Application.UpdateEmployee;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateEmployeeCommand command)
        {
            Employee employee = await _mediator.Send(command);
            if (employee != null)
            {
                return Ok(employee);
            }
            return BadRequest(new { message = "Failed to create Employee" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            GetAllEmployeeQuery query = new GetAllEmployeeQuery();
            List<Employee> employees = await _mediator.Send(query);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            GetEmployeeByIdQuery query = new GetEmployeeByIdQuery();
            query.Id = id;
            Employee employee = await _mediator.Send(query);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound(new { message = $"No Employee found with id {id}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, UpdateEmployeeCommand command)
        {
            command.Id = id;
            Employee updatedEmployee = await _mediator.Send(command);
            if (updatedEmployee != null)
            {
                return Ok(updatedEmployee);
            }
            else
            {
                return NotFound(new { message = $"Employee not found with the id {id}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            DeleteEmployeeCommand query = new DeleteEmployeeCommand();
            query.Id = id;
            int response = await _mediator.Send(query);
            if (response == 1)
            {
                return Ok(new { message = "Successfully deleted employee" });
            }
            else
            {
                return NotFound(new { message = "Failed to delete employee" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> AdminLogin(LoginRequestCommand command)
        {
            var adminResponse = await _mediator.Send(command);
            if (adminResponse != null)
            {
                return Ok(adminResponse);
            } else
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}
