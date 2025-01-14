using MediatR;
using FluentValidation;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Common.Exceptions;

namespace Vehicle.Application.Features.JobPostings.Commands.Handlers
{
    public class UpdateJobPostingHandler : IRequestHandler<UpdateJobPostingCommand, int>
    {
        private readonly IJobPostingRepository _repository;
        private readonly IValidator<JobPosting> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobPostingHandler> _logger;

        public UpdateJobPostingHandler(IJobPostingRepository repository, IValidator<JobPosting> validator, IMapper mapper, ILogger<UpdateJobPostingHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var jobPosting = await _repository.GetByIdAsync(request.Id);
            if (jobPosting == null)
            {
                throw new NotFoundException(nameof(JobPosting), request.Id);
            }

            _mapper.Map(request, jobPosting);

            var validationResult = _validator.Validate(jobPosting);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _repository.UpdateAsync(jobPosting);
            _logger.LogInformation($"JobPosting {jobPosting.Id} updated successfully.");
            return jobPosting.Id;
        }
    }
}
