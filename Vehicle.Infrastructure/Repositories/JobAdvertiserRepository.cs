using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Infrastructure.Persistence;

namespace Vehicle.Infrastructure.Repositories
{
    public class JobAdvertiserRepository : RepositoryBase<JobAdvertiser>, IJobAdvertiserRepository
    {
        public JobAdvertiserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<JobAdvertiser>> GetByCompanyNameAsync(string companyName, int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(ja => ja.CompanyName.Contains(companyName), pageNumber: pageNumber, pageSize: pageSize);
        }

        public async Task<IReadOnlyList<JobAdvertiser>> GetOrderedByContactEmailAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(
                predicate: null,
                orderBy: query => query.OrderBy(ja => ja.ContactEmail),
                includes: (string[])null,  // Use string[] includes
                disableTracking: true,
                pageNumber: pageNumber,
                pageSize: pageSize
            );
        }


        public async Task<IReadOnlyList<JobAdvertiser>> GetWithPostedJobsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var includes = new string[] { "JobPostings" };
            return await GetAsync(
                predicate: null,
                orderBy: null,
                includes: includes,
                disableTracking: true,
                pageNumber: pageNumber,
                pageSize: pageSize
            );
        }


        public async Task<JobAdvertiser> GetByContactEmailAsync(string email)
        {
            var jobAdvertisers = await GetAsync(ja => ja.ContactEmail == email, orderBy: null, 
                includes: new string[] { },  //[jobseeker, advertiser,...] could include any relaed table
                disableTracking: true);
            return jobAdvertisers.FirstOrDefault();
        }
    }
}
