using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Infrastructure.Persistence;

namespace Vehicle.Infrastructure.Repositories
{
    public class JobPostingRepository : RepositoryBase<JobPosting>, IJobPostingRepository
    {
        public JobPostingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
