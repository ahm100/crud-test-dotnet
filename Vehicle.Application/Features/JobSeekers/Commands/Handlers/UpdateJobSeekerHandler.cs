using MediatR;
using FluentValidation;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Common.Exceptions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vehicle.Application.Features.JobSeekers.Commands.Handlers
{
    public class UpdateJobSeekerHandler : IRequestHandler<UpdateJobSeekerCommand, int>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IValidator<JobSeeker> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobSeekerHandler> _logger;

        public UpdateJobSeekerHandler(IJobSeekerRepository repository, IValidator<JobSeeker> validator, IMapper mapper, ILogger<UpdateJobSeekerHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateJobSeekerCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetByIdAsync(request.Id);
            if (jobSeeker == null)
            {
                throw new NotFoundException(nameof(JobSeeker), request.Id);
            }

            _mapper.Map(request, jobSeeker);

            var validationResult = _validator.Validate(jobSeeker);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _repository.UpdateAsync(jobSeeker);
            _logger.LogInformation($"JobSeeker {jobSeeker.Id} updated successfully.");
            return jobSeeker.Id;
        }
    }
}
