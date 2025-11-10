
using Hotel.Application.CreateReview;
using Hotel.Application.DeleteReview;
using Hotel.Application.GetAllReviews;
using Hotel.Application.GetReviewById;
using Hotel.Application.UpdateReview;
using Hotel.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            GetAllReviewQuery query = new GetAllReviewQuery();
            List<Review> reviews = await _mediator.Send(query);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            GetReviewByIdQuery query = new GetReviewByIdQuery();
            query.Id = id;
            Review review = await _mediator.Send(query);
            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            Review newReview = await _mediator.Send(command);
            if (newReview != null)
            {
                return Ok(newReview);
            }
            else
            {
                return BadRequest();

            }

        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewCommand command)
        {
            command.Id = id;
            Review review = await _mediator.Send(command);
            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int id)
        {
            DeleteReviewCommand deleteReviewCommand = new DeleteReviewCommand();
            deleteReviewCommand.Id = id;
            int response = await _mediator.Send(deleteReviewCommand);
            if (response != 0)
            {
                return Ok("Review deleted successfully");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
