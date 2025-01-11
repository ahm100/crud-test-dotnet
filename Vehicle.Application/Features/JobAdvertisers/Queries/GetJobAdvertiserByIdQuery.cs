using MediatR;
using Vehicle.Application.Features.JobAdvertisers.Dtos;

namespace Vehicle.Application.Features.JobAdvertisers.Queries
{
    public class GetJobAdvertiserByIdQuery : IRequest<JobAdvertiserDto>
    {
        public int Id { get; set; }
    }
}
