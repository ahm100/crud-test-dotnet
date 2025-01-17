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
    public class GetJobSeekersByBirthYearQueryHandler : IRequestHandler<GetJobSeekersByBirthYearQuery, List<JobSeekerDto>>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public GetJobSeekersByBirthYearQueryHandler(IJobSeekerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobSeekerDto>> Handle(GetJobSeekersByBirthYearQuery request, CancellationToken cancellationToken)
        {
            var jobSeekers = await _repository.GetByBirthYearAsync(request.Year, request.PageNumber, request.PageSize);
            return _mapper.Map<List<JobSeekerDto>>(jobSeekers);
        }
    }
}
