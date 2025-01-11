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
using Vehicle.Application.Common.Exceptions;

namespace Vehicle.Application.Features.JobAdvertisers.Commands.Hnadlers
{
    public class UpdateJobAdvertiserHandler : IRequestHandler<UpdateJobAdvertiserCommand>
    {
        private readonly IJobAdvertiserRepository _repository;
        private readonly IValidator<UpdateJobAdvertiserCommand> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobAdvertiserHandler> _logger;

        public UpdateJobAdvertiserHandler(IJobAdvertiserRepository repository, IValidator<UpdateJobAdvertiserCommand> validator, IMapper mapper, ILogger<UpdateJobAdvertiserHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateJobAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var jobAdvertiser = await _repository.GetByIdAsync(request.Id);
            if (jobAdvertiser == null)
            {
                throw new NotFoundException(nameof(JobAdvertiser), request.Id);
            }

            _mapper.Map(request, jobAdvertiser);
            await _repository.UpdateAsync(jobAdvertiser);
            _logger.LogInformation($"JobAdvertiser {jobAdvertiser.Id} updated successfully.");
            return Unit.Value;
        }
    }
}
