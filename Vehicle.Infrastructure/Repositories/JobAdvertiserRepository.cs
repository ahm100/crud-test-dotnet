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

        public async Task<IReadOnlyList<JobAdvertiser>> GetByCompanyNameAsync(string companyName)
        {
            return await GetAsync(ja => ja.CompanyName == companyName);
        }

        public async Task<IReadOnlyList<JobAdvertiser>> GetOrderedByContactEmailAsync()
        {
            return await GetAsync(predicate: null, orderBy: query => query.OrderBy(ja => ja.ContactEmail), includeString: null);
        }

        public async Task<IReadOnlyList<JobAdvertiser>> GetWithPostedJobsAsync()
        {
            var includes = new List<Expression<Func<JobAdvertiser, object>>>
            {
                ja => ja.JobPostings
            };
            return await GetAsync(predicate: null, orderBy: null, includes: includes);
        }

        public async Task<JobAdvertiser> GetByContactEmailAsync(string email)
        {
            return (await GetAsync(ja => ja.ContactEmail == email)).FirstOrDefault();
        }
    }
}
