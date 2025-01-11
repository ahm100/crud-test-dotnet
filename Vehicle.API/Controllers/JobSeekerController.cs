using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Application.Features.JobSeekers.Commands;

namespace Vehicle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobSeekerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobSeekerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
