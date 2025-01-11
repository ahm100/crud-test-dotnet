using AutoMapper;
using Vehicle.Application.Features.JobAdvertisers.Commands;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Mapping
{
    public class JobAdvertiserProfile : Profile
    {
        public JobAdvertiserProfile()
        {
            CreateMap<CreateJobAdvertiserCommand, JobAdvertiser>();
        }
    }
}
