using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobSeekers.Commands;
using Vehicle.Application.Features.JobSeekers.Queries;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobSeekersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobSeekerDto>> GetJobSeekerById(int id)
        {
            var jobSeeker = await _mediator.Send(new GetJobSeekerByIdQuery(id));
            if (jobSeeker == null)
            {
                return NotFound();
            }
            return Ok(jobSeeker);
        }

        [HttpGet("with-related-data/{id}")]
        public async Task<ActionResult<JobSeekerDto>> GetJobSeekerByIdWithRelatedData(int id)
        {
            var jobSeeker = await _mediator.Send(new GetJobSeekerByIdWithRelatedDataQuery(id));
            if (jobSeeker == null)
            {
                return NotFound();
            }
            return Ok(jobSeeker);
        }

        [HttpGet("skill/{skill}")]
        public async Task<ActionResult<List<JobSeekerDto>>> GetJobSeekersBySkill(string skill, int pageNumber = 1, int pageSize = 10)
        {
            var jobSeekers = await _mediator.Send(new GetJobSeekersBySkillQuery(skill, pageNumber, pageSize));
            return Ok(jobSeekers);
        }

        [HttpGet("skills")]
        public async Task<ActionResult<List<JobSeekerDto>>> GetJobSeekersBySomeSkills([FromQuery] List<string> skills, int pageNumber = 1, int pageSize = 10)
        {
            var jobSeekers = await _mediator.Send(new GetJobSeekersBySomeSkillsQuery(skills)
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(jobSeekers);
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateJobSeeker(CreateJobSeekerCommand command)
        {
            var jobSeekerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetJobSeekerById), new { id = jobSeekerId }, jobSeekerId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobSeeker(int id, UpdateJobSeekerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            await _mediator.Send(new DeleteJobSeekerCommand { Id = id });
            return NoContent();
        }
    }
}
