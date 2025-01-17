using MediatR;
using Vehicle.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Features.JobSeekers.Commands.Handlers
{
    public class DeleteJobSeekerHandler : IRequestHandler<DeleteJobSeekerCommand>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly ILogger<DeleteJobSeekerHandler> _logger;

        public DeleteJobSeekerHandler(IJobSeekerRepository repository, ILogger<DeleteJobSeekerHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteJobSeekerCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetByIdAsync(request.Id);
            if (jobSeeker == null)
            {
                throw new NotFoundException(nameof(JobSeeker), request.Id);
            }

            await _repository.DeleteAsync(jobSeeker);
            _logger.LogInformation($"JobSeeker {jobSeeker.Id} deleted successfully.");

            return Unit.Value;
        }
    }
}
