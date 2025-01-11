using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IReadOnlyList<JobPosting>> GetByTitleAsync(string title, int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(jp => jp.Title.Contains(title), pageNumber: pageNumber, pageSize: pageSize);
        }

        public async Task<IReadOnlyList<JobPosting>> GetOrderedByDatePostedAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(predicate: null, orderBy: query => query.OrderBy(jp => jp.PostDate), includeString: null, disableTracking: true, pageNumber: pageNumber, pageSize: pageSize);
        }

        public async Task<IReadOnlyList<JobPosting>> GetWithAdvertiserAsync(int pageNumber = 1, int pageSize = 10)
        {
            var includes = new List<Expression<Func<JobPosting, object>>>
            {
                jp => jp.JobAdvertiser
            };
            return await GetAsync(predicate: null, orderBy: null, includes: includes, disableTracking: true, pageNumber: pageNumber, pageSize: pageSize);
        }

        public async Task<JobPosting> GetByReferenceNumberAsync(string referenceNumber)
        {
            var jobPostings = await GetAsync(jp => jp.ReferenceNumber == referenceNumber, orderBy: null, includeString: null, disableTracking: true);
            return jobPostings.FirstOrDefault();
        }

        public async Task<IReadOnlyList<JobPosting>> GetByCategoryIdAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(jp => jp.JobCategoryId == categoryId, pageNumber: pageNumber, pageSize: pageSize);
        }
    }
}
