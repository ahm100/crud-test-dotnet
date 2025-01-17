using MediatR;
using System.Collections.Generic;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Queries
{
    public class GetJobSeekersBySomeSkillsQuery : IRequest<List<JobSeekerDto>>
    {
        public List<string> Skills { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetJobSeekersBySomeSkillsQuery(List<string> skills)
        {
            Skills = skills;
        }
    }
}
