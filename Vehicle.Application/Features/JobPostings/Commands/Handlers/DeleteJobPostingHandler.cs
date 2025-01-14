using MediatR;
using Vehicle.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Common.Exceptions;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Features.JobPostings.Commands.Handlers
{
    public class DeleteJobPostingHandler : IRequestHandler<DeleteJobPostingCommand, int>
    {
        private readonly IJobPostingRepository _repository;
        private readonly ILogger<DeleteJobPostingHandler> _logger;

        public DeleteJobPostingHandler(IJobPostingRepository repository, ILogger<DeleteJobPostingHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            var jobPosting = await _repository.GetByIdAsync(request.Id);
            if (jobPosting == null)
            {
                throw new NotFoundException(nameof(JobPosting), request.Id);
            }

            await _repository.DeleteAsync(jobPosting);
            _logger.LogInformation($"JobPosting {jobPosting.Id} deleted successfully.");
            return jobPosting.Id;
        }
    }
}
