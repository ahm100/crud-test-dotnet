using MediatR;
using System;
using System.Collections.Generic;
using Vehicle.Application.Features.JobSeekers.Dtos;

namespace Vehicle.Application.Features.JobSeekers.Commands
{
    public class CreateJobSeekerCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Resume { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public List<JobSeekerSkillDto> Skills { get; set; }// = new List<JobSeekerSkillDto>(); handle by mapper
        public List<JobSeekerExperienceDto> Experience { get; set; }// = new List<JobSeekerExperienceDto>();
    }
}
