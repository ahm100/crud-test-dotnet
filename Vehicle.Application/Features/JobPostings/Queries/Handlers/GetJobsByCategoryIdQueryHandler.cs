
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
    public class GetJobsByCategoryIdQueryHandler : IRequestHandler<GetJobsByCategoryIdQuery, List<JobPostingDto>>
    {
        private readonly IJobPostingRepository _repository;
        private readonly IMapper _mapper;

        public GetJobsByCategoryIdQueryHandler(IJobPostingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobPostingDto>> Handle(GetJobsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var jobPostings = await _repository.GetAsync(jp => jp.JobCategoryId == request.CategoryId);
            return _mapper.Map<List<JobPostingDto>>(jobPostings);
        }
    }
}
