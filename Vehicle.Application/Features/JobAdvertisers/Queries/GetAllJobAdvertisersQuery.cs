using MediatR;
using System.Collections.Generic;
using Vehicle.Application.Features.JobAdvertisers.Dtos;

namespace Vehicle.Application.Features.JobAdvertisers.Queries
{
    public class GetAllJobAdvertisersQuery : IRequest<List<JobAdvertiserDto>> { }
}
