using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobSeekers.Commands;
using Vehicle.Application.Features.JobSeekers.Queries;
using Vehicle.Application.Features.JobSeekers.Dtos;
using Microsoft.Extensions.Localization;
using Vehicle.Application.Resources;
using System.Globalization;
using Vehicle.Application.Features.JobAdvertisers.Commands.Handlers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Vehicle.API.SharedResources;

namespace Vehicle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekersController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IStringLocalizer<JobSeekerResource> _localizer;
        private readonly IStringLocalizer<JobSeekersController> _localizer;
        private readonly IStringLocalizer<SharedTranslate> _sharedLocalizer;

        private readonly ILogger<JobSeekersController> _logger;

        public JobSeekersController(IMediator mediator,
            //IStringLocalizer<JobSeekerResource> localizer,
            IStringLocalizer<JobSeekersController> localizer,
            IStringLocalizer<SharedTranslate> sharedLocalizer,
            ILogger<JobSeekersController> logger)
        {
            _mediator = mediator;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobSeekerDto>> GetJobSeekerById(int id, [FromQuery] string culture = "fa")
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
        public async Task<ActionResult<int>> CreateJobSeeker([FromBody] CreateJobSeekerCommand command, [FromQuery] string culture = "fa")
        {
            var currentCulture = new CultureInfo(culture);
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;

            try
            {
                var jobSeekerId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetJobSeekerById), new { id = jobSeekerId }, jobSeekerId);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogError($"Validation error: {string.Join(", ", errors)}");
                //return BadRequest(new { Errors = errors });
                return BadRequest(new { Errors = _localizer["GeneralError"] });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create job seeker: {ex.Message}");
                return StatusCode(500, _localizer["InternalServerError"]);
            }
        }

        [AllowAnonymous]
        [HttpGet("test-localization")]
        public ActionResult TestLocalization([FromQuery] string culture = "fa")
        {
            var currentCulture = new CultureInfo(culture);
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;

            var message = _sharedLocalizer["shared message"];
            //var message = _localizer["Hello World"];
            return Ok(new { Message = message });
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
