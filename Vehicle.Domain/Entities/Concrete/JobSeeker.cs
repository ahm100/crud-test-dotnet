using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Common;

namespace Vehicle.Domain.Entities.Concrete
{
    public class JobSeeker :  EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Resume { get; set; } // Path to resume file       
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }// E.164 format
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public List<JobSeekerSkill> Skills { get; set; }
        public List<JobSeekerExperience> Experience { get; set; }
    }
}
