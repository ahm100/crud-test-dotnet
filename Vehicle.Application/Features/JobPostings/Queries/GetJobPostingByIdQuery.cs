using MediatR;
using Vehicle.Application.Features.JobPostings.Dtos;

namespace Vehicle.Application.Features.JobPostings.Queries
{
    public class GetJobPostingByIdQuery : IRequest<JobPostingDto>
    {
        public int Id { get; set; }
    }
}
