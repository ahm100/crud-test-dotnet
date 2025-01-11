using System.Collections.Generic;
using Vehicle.Domain.Common;

namespace Vehicle.Domain.Entities.Concrete
{
    public class JobCategory : EntityBase
    {
        public string CategoryName { get; set; }

        // Navigation property to represent the relationship with JobPostings
        public List<JobPosting> JobPostings { get; set; }
    }
}
