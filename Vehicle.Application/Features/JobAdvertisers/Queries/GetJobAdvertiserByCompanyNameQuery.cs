using MediatR;
using System.Collections.Generic;
using Vehicle.Application.Features.JobAdvertisers.Dtos;

namespace Vehicle.Application.Features.JobAdvertisers.Queries
{
    public class GetJobAdvertiserByCompanyNameQuery : IRequest<List<JobAdvertiserDto>>
    {
        public string CompanyName { get; set; }
    }
}
