using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Common;

namespace Vehicle.Domain.Entities.Concrete
{
    public class JobSeekerExperience : EntityBase
    {
        [ForeignKey("JobSeeker")]
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public string Experience { get; set; }
    }
}
