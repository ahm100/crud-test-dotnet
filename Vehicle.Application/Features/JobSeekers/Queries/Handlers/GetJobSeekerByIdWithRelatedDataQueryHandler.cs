using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Application.Common.Exceptions;
using Vehicle.Domain.Entities.Concrete;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Queries.Handlers
{
    public class GetJobSeekerByIdWithRelatedDataQueryHandler : IRequestHandler<GetJobSeekerByIdWithRelatedDataQuery, JobSeekerDto>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public GetJobSeekerByIdWithRelatedDataQueryHandler(IJobSeekerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobSeekerDto> Handle(GetJobSeekerByIdWithRelatedDataQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetByIdWithRelatedDataAsync(request.Id);
            if (jobSeeker == null)
            {
                throw new NotFoundException(nameof(JobSeeker), request.Id);
            }

            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }
    }
}
