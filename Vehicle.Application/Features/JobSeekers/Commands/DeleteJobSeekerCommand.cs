using MediatR;

namespace Vehicle.Application.Features.JobSeekers.Commands
{
    public class DeleteJobSeekerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
