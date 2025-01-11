


using MediatR;
using Vehicle.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Common.Exceptions;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Features.JobAdvertisers.Commands.Handlers
{
    public class DeleteJobAdvertiserHandler : IRequestHandler<DeleteJobAdvertiserCommand>
    {
        private readonly IJobAdvertiserRepository _repository;
        private readonly ILogger<DeleteJobAdvertiserHandler> _logger;

        public DeleteJobAdvertiserHandler(IJobAdvertiserRepository repository, ILogger<DeleteJobAdvertiserHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteJobAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var jobAdvertiser = await _repository.GetByIdAsync(request.Id);
            if (jobAdvertiser == null)
            {
                throw new NotFoundException(nameof(JobAdvertiser), request.Id);
            }

            await _repository.DeleteAsync(jobAdvertiser);
            _logger.LogInformation($"JobAdvertiser {jobAdvertiser.Id} deleted successfully.");
            return Unit.Value;
        }
    }
}
