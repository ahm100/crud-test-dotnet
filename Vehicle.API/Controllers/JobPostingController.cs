using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Application.Features.JobPostings.Commands;
using Vehicle.Application.Features.JobPostings.Queries;

namespace Vehicle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobPostingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobPostingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetJobPostingByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllJobPostingsQuery());
            return Ok(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _mediator.Send(new GetJobsByCategoryIdQuery { CategoryId = categoryId });
            return Ok(result);
        }

        [HttpGet("with-advertiser")]
        public async Task<IActionResult> GetWithAdvertiser()
        {
            var result = await _mediator.Send(new GetJobPostingsWithAdvertiserQuery());
            return Ok(result);
        }

        [HttpGet("ordered-by-date")]
        public async Task<IActionResult> GetOrderedByDate()
        {
            var result = await _mediator.Send(new GetJobPostingsOrderedByDateQuery());
            return Ok(result);
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var result = await _mediator.Send(new GetJobPostingsByTitleQuery { Title = title });
            return Ok(result);
        }
    }
}
