using MediatR;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Queries
{
    public class GetJobSeekerByIdQuery : IRequest<JobSeekerDto>
    {
        public int Id { get; set; }

        public GetJobSeekerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
