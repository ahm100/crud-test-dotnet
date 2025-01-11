using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Application.Features.JobAdvertisers.Queries;
using Vehicle.Application.Common.Exceptions;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Application.Features.JobAdvertisers.Dtos;

namespace Vehicle.Application.Features.JobAdvertisers.Queries.Handlers
{
    public class GetJobAdvertiserByIdQueryHandler : IRequestHandler<GetJobAdvertiserByIdQuery, JobAdvertiserDto>
    {
        private readonly IJobAdvertiserRepository _repository;
        private readonly IMapper _mapper;

        public GetJobAdvertiserByIdQueryHandler(IJobAdvertiserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobAdvertiserDto> Handle(GetJobAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            var jobAdvertiser = await _repository.GetByIdAsync(request.Id);
            if (jobAdvertiser == null)
            {
                throw new NotFoundException(nameof(JobAdvertiser), request.Id);
            }

            return _mapper.Map<JobAdvertiserDto>(jobAdvertiser);
        }
    }
}
