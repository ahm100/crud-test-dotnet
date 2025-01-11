using MediatR;
using FluentValidation;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Vehicle.Application.Features.JobPostings.Commands.Handlers
{
    public class CreateJobPostingHandler : IRequestHandler<CreateJobPostingCommand, int>
    {
        private readonly IJobPostingRepository _repository;
        private readonly IValidator<JobPosting> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobPostingHandler> _logger;

        public CreateJobPostingHandler(IJobPostingRepository repository, IValidator<JobPosting> validator, IMapper mapper, ILogger<CreateJobPostingHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var jobPosting = _mapper.Map<JobPosting>(request);

            var validationResult = _validator.Validate(jobPosting);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _repository.AddAsync(jobPosting);
            _logger.LogInformation($"JobPosting {result.Id} created successfully.");
            return result.Id;
        }
    }
}
