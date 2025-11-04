using Hotel.Application.Hotels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HotelController(IMediator mediator) { 
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<int> CreateHotel(CreateHotelCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
