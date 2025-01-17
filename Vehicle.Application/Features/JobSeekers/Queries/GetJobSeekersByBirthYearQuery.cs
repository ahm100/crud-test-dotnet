using MediatR;
using System.Collections.Generic;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Queries
{
    public class GetJobSeekersByBirthYearQuery : IRequest<List<JobSeekerDto>>
    {
        public int Year { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
