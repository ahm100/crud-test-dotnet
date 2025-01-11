using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Application.Features.JobPostings.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobPostings.Dtos;

namespace Vehicle.Application.Features.JobPostings.Queries.Handlers
{
    public class GetAllJobPostingsQueryHandler : IRequestHandler<GetAllJobPostingsQuery, List<JobPostingDto>>
    {
        private readonly IJobPostingRepository _repository;
        private readonly IMapper _mapper;

        public GetAllJobPostingsQueryHandler(IJobPostingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobPostingDto>> Handle(GetAllJobPostingsQuery request, CancellationToken cancellationToken)
        {
            var jobPostings = await _repository.GetAllAsync();
            return _mapper.Map<List<JobPostingDto>>(jobPostings);
        }
    }
}
