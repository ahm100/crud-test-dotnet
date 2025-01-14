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

            // Generate ReferenceNumber using the created ID
            result.ReferenceNumber = GenerateReferenceNumber(result.Id);

            // Update the job posting with the generated ReferenceNumber
            await _repository.UpdateAsync(result);

            _logger.LogInformation($"JobPosting {result.Id} created successfully.");
            return result.Id;
        }

        private string GenerateReferenceNumber(int jobPostingId)
        {
            var guidPart = Guid.NewGuid().ToString("N").Substring(0, 8); // Shortened UUID and (without hyphens)
            var timestampPart = DateTime.UtcNow.ToString("yyyyMMddHHmmss"); // Timestamp
            var customPart = $"JP-{jobPostingId:D6}"; // Custom format with ID
            //The D6 format specifier is used to pad the integer value with leading zeros, making it at least 6 digits long. like "000123"

            //return $"{customPart}-{guidPart}-{timestampPart}";
            return $"{customPart}-{guidPart}";
        }



    }
}
