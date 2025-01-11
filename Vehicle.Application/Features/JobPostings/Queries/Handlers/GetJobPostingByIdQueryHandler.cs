
using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Application.Features.JobPostings.Queries;
using Vehicle.Application.Features.JobPostings.Dtos;

namespace Vehicle.Application.Features.JobPostings.Queries.Handlers
{
    public class GetJobPostingByIdQueryHandler : IRequestHandler<GetJobPostingByIdQuery, JobPostingDto>
    {
        private readonly IJobPostingRepository _repository;
        private readonly IMapper _mapper;

        public GetJobPostingByIdQueryHandler(IJobPostingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobPostingDto> Handle(GetJobPostingByIdQuery request, CancellationToken cancellationToken)
        {
            var jobPosting = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<JobPostingDto>(jobPosting);
        }
    }
}
