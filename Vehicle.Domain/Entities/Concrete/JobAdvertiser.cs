using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Common;

namespace Vehicle.Domain.Entities.Concrete
{
    public class JobAdvertiser : EntityBase
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; } // E.164 format 

        // Navigation property to represent the relationship with Job postings
        public List<JobPosting> JobPostings { get; set; }
    }
}