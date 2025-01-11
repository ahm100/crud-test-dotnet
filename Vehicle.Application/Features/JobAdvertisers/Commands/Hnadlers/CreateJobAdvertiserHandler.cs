using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Vehicle.Application.Features.JobAdvertisers.Commands.Hnadlers
{
    public class CreateJobAdvertiserHandler : IRequestHandler<CreateJobAdvertiserCommand, int>
    {
        private readonly IJobAdvertiserRepository _repository;
        private readonly IValidator<JobAdvertiser> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobAdvertiserHandler> _logger;

        public CreateJobAdvertiserHandler(IJobAdvertiserRepository repository, IValidator<JobAdvertiser> validator, IMapper mapper, ILogger<CreateJobAdvertiserHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateJobAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var jobAdvertiser = _mapper.Map<JobAdvertiser>(request);

            var validationResult = _validator.Validate(jobAdvertiser);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _repository.AddAsync(jobAdvertiser);
            _logger.LogInformation($"JobAdvertiser {result.Id} created successfully.");
            return result.Id;
        }
    }
}
