using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Application.Features.JobAdvertisers.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobAdvertisers.Dtos;

namespace Vehicle.Application.Features.JobAdvertisers.Queries.Handlers
{
    public class GetAllJobAdvertisersQueryHandler : IRequestHandler<GetAllJobAdvertisersQuery, List<JobAdvertiserDto>>
    {
        private readonly IJobAdvertiserRepository _repository;
        private readonly IMapper _mapper;

        public GetAllJobAdvertisersQueryHandler(IJobAdvertiserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobAdvertiserDto>> Handle(GetAllJobAdvertisersQuery request, CancellationToken cancellationToken)
        {
            var jobAdvertisers = await _repository.GetAllAsync();
            return _mapper.Map<List<JobAdvertiserDto>>(jobAdvertisers);
        }
    }
}
