using Hotel.Application.Admin_logout.Command;
using Hotel.Application.CreateEmployee;
using Hotel.Application.DeleteEmployee;
using Hotel.Application.EmployeeGetAll;
using Hotel.Application.GetAllEmployee;
using Hotel.Application.GetAllEmployeeById;
using Hotel.Application.LoginRequest;
using Hotel.Application.RefreshToken;
using Hotel.Application.UpdateEmployee;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            ApiResponse<EmployeeGetAllDto> employee = await _mediator.Send(command);
            if (employee.Success == true)
            {
                return Ok(employee);
            }
            else
            {
                return BadRequest(employee);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            GetAllEmployeeQuery query = new GetAllEmployeeQuery();
            List<EmployeeGetAllDto> employees = await _mediator.Send(query);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            GetEmployeeByIdQuery query = new GetEmployeeByIdQuery();
            query.Id = id;
            EmployeeGetAllDto employee = await _mediator.Send(query);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound(new { message = $"No Employee found with id {id}" });
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeCommand command)
        {
            command.Id = id;
            ApiResponse<EmployeeGetAllDto> updatedEmployee = await _mediator.Send(command);
            if (updatedEmployee.Success == true)
            {
                return Ok(updatedEmployee);
            }
            else
            {
                return NotFound(updatedEmployee);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
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
            if (adminResponse.Success == true)
            {
                return Ok(adminResponse);
            }
            else
            {
                return BadRequest(adminResponse);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var reponse = await _mediator.Send(command);
            if (reponse.Success == true)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok(new {authenticated = true});
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            AdminLogoutCommand command = new AdminLogoutCommand();
            var logoutRespones = _mediator.Send(command);
            if(logoutRespones == null)
            {
                return BadRequest();
            }
            return Ok(new {message = logoutRespones});
        }

    }
}
