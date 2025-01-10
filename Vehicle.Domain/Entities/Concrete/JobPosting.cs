using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Common;

namespace Vehicle.Domain.Entities.Concrete
{
    public class JobPosting : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
        public DateTime? ExpiryDate { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; } 
        
        // Foreign key to JobAdvertiser
        public int JobAdvertiserId { get; set; }
        public JobAdvertiser JobAdvertiser { get; set; } // Navigation property 
    }
}
