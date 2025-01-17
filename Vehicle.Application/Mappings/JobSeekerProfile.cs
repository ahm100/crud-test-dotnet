using AutoMapper;
using Vehicle.Application.Features.JobSeekers.Commands;
using Vehicle.Application.Features.JobSeekers.Dtos;
//using Vehicle.Application.Features.JobSeekers.Queries;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Mapping
{
    public class JobSeekerProfile : Profile
    {
        public JobSeekerProfile()
        {
            CreateMap<CreateJobSeekerCommand, JobSeeker>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkill { SkillName = skill.SkillName }).ToList()))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperience { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate }).ToList()));

            CreateMap<UpdateJobSeekerCommand, JobSeeker>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkill { SkillName = skill.SkillName }).ToList()))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperience { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate }).ToList()));

            CreateMap<JobSeeker, JobSeekerDto>()
               .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkillDto { SkillName = skill.SkillName })))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperienceDto { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate })));

        }
    }
}
