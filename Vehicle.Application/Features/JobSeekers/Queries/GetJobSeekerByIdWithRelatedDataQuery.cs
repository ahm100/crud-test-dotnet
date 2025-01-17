using MediatR;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Queries
{
    public class GetJobSeekerByIdWithRelatedDataQuery : IRequest<JobSeekerDto>
    {
        public int Id { get; set; }

        public GetJobSeekerByIdWithRelatedDataQuery(int id)
        {
            Id = id;
        }
    }
}
