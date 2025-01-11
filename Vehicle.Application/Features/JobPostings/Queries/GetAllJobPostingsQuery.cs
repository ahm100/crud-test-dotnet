
using MediatR;
using System.Collections.Generic;
using Vehicle.Application.Features.JobPostings.Dtos;

namespace Vehicle.Application.Features.JobPostings.Queries
{
    public class GetAllJobPostingsQuery : IRequest<List<JobPostingDto>> { }
}
