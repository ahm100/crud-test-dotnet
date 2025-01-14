using MediatR;

namespace Vehicle.Application.Features.JobPostings.Commands
{
    public class DeleteJobPostingCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteJobPostingCommand(int id)
        {
            Id = id;
        }
    }
}
