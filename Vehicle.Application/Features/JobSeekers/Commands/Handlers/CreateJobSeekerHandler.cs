using MediatR;
using FluentValidation;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Vehicle.Application.Features.JobSeekers.Commands.Handlers
{
    public class CreateJobSeekerHandler : IRequestHandler<CreateJobSeekerCommand, int>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IValidator<JobSeeker> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobSeekerHandler> _logger;

        public CreateJobSeekerHandler(IJobSeekerRepository repository, IValidator<JobSeeker> validator, IMapper mapper, ILogger<CreateJobSeekerHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateJobSeekerCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = _mapper.Map<JobSeeker>(request);

            var validationResult = _validator.Validate(jobSeeker);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _repository.AddAsync(jobSeeker);
            _logger.LogInformation($"JobSeeker {result.Id} created successfully.");
            return result.Id;
        }
    }
}
