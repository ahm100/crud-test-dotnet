using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Application.Features.JobAdvertisers.Commands;

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
    }
}
