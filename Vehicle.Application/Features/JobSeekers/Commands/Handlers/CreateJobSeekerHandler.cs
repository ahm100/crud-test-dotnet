using MediatR;
using FluentValidation;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Vehicle.Application.Resources;

namespace Vehicle.Application.Features.JobSeekers.Commands.Handlers
{
    public class CreateJobSeekerHandler : IRequestHandler<CreateJobSeekerCommand, int>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IValidator<CreateJobSeekerCommand> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobSeekerHandler> _logger;
        private readonly IStringLocalizer<JobSeekerResource> _localizer;

        public CreateJobSeekerHandler(IJobSeekerRepository repository, IValidator<CreateJobSeekerCommand> validator, IMapper mapper, ILogger<CreateJobSeekerHandler> logger, IStringLocalizer<JobSeekerResource> localizer)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<int> Handle(CreateJobSeekerCommand request, CancellationToken cancellationToken)
        {
            // Check if the email already exists
            var existingJobSeeker = await _repository.GetByEmailAsync(request.Email);
            if (existingJobSeeker != null)
            {
                throw new ValidationException(_localizer["EmailAlreadyExists"]);
            }

            var jobSeeker = _mapper.Map<JobSeeker>(request);

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _repository.AddAsync(jobSeeker);
            _logger.LogInformation(_localizer["ExperienceAdded"]);
            return result.Id;
        }

    }
}
