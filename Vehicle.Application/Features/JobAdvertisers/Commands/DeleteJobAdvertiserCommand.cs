using MediatR;

namespace Vehicle.Application.Features.JobAdvertisers.Commands
{
    public class DeleteJobAdvertiserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
