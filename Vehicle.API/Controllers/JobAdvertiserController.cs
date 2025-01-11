using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Application.Features.JobAdvertisers.Commands;
using Vehicle.Application.Features.JobAdvertisers.Queries;

namespace Vehicle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdvertiserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobAdvertiserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobAdvertiserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJobAdvertiserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetJobAdvertiserByIdQuery { Id = id });
            return Ok(result);
        }
    }
}
