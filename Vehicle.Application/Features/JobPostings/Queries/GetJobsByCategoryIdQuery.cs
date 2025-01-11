using MediatR;
using System.Collections.Generic;
using Vehicle.Application.Features.JobPostings.Dtos;

namespace Vehicle.Application.Features.JobPostings.Queries
{
    public class GetJobsByCategoryIdQuery : IRequest<List<JobPostingDto>>
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
