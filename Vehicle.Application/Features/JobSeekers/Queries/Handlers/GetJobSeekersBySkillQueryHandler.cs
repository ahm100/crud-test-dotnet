using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Queries.Handlers
{
    public class GetJobSeekersBySkillQueryHandler : IRequestHandler<GetJobSeekersBySkillQuery, List<JobSeekerDto>>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public GetJobSeekersBySkillQueryHandler(IJobSeekerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobSeekerDto>> Handle(GetJobSeekersBySkillQuery request, CancellationToken cancellationToken)
        {
            var jobSeekers = await _repository.GetBySkillAsync(request.Skill, request.PageNumber, request.PageSize);
            return _mapper.Map<List<JobSeekerDto>>(jobSeekers);
        }
    }
}
