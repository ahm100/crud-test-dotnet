using MediatR;
using AutoMapper;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Application.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Vehicle.Application.Features.JobSeekers.Dtos;
using Microsoft.Extensions.Localization;
using Vehicle.Application.Resources;
using Vehicle.Application.Contracts;
using Vehicle.Shared.Resources;

namespace Vehicle.Application.Features.JobSeekers.Queries.Handlers
{
    public class GetJobSeekerByIdQueryHandler : IRequestHandler<GetJobSeekerByIdQuery, JobSeekerDto>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;
        //private readonly IStringLocalizer<JobSeekerResource> _localizer;
        private readonly ISharedLocalizer<Vehicle.Shared.Resources.JobSeekersResource> _localizer;

        public GetJobSeekerByIdQueryHandler(IJobSeekerRepository repository, 
            IMapper mapper,
            //IStringLocalizer<JobSeekerResource> localizer
            ISharedLocalizer<Vehicle.Shared.Resources.JobSeekersResource> localizer

            )
        {
            _repository = repository;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<JobSeekerDto> Handle(GetJobSeekerByIdQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetByIdAsync(request.Id);
            if (jobSeeker == null)
            {
                //throw new NotFoundException(nameof(JobSeeker), request.Id);
                throw new NotFoundException(_localizer["Hello World"]);
            }

            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }
    }
}
