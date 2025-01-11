using AutoMapper;
using Vehicle.Application.Features.JobAdvertisers.Commands;
using Vehicle.Application.Features.JobAdvertisers.Dtos;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Mapping
{
    public class JobAdvertiserProfile : Profile
    {
        public JobAdvertiserProfile()
        {
            CreateMap<CreateJobAdvertiserCommand, JobAdvertiser>();
            CreateMap<UpdateJobAdvertiserCommand, JobAdvertiser>(); 
            CreateMap<JobAdvertiser, JobAdvertiserDto>();

        }
    }
}
