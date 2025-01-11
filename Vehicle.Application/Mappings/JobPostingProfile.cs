using AutoMapper;
using Vehicle.Application.Features.JobPostings.Commands;
using Vehicle.Application.Features.JobPostings.Dtos;
//using Vehicle.Application.Features.JobPostings.Queries;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Mapping
{
    public class JobPostingProfile : Profile
    {
        public JobPostingProfile()
        {
            CreateMap<CreateJobPostingCommand, JobPosting>();

            CreateMap<JobPosting, JobPostingDto>()
                .ForMember(dest => dest.JobAdvertiserName, opt => opt.MapFrom(src => src.JobAdvertiser.CompanyName))
                .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.JobCategory.CategoryName));
        }
    }
}
