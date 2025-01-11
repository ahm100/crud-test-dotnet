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

        public async Task<IReadOnlyList<JobPosting>> GetByTitleAsync(string title)
        {
            return await GetAsync(jp => jp.Title.Contains(title));
        }

        public async Task<IReadOnlyList<JobPosting>> GetOrderedByDatePostedAsync()
        {
            return await GetAsync(predicate: null, orderBy: query => query.OrderBy(jp => jp.PostDate), includeString: null);
        }

        public async Task<IReadOnlyList<JobPosting>> GetWithAdvertiserAsync()
        {
            var includes = new List<Expression<Func<JobPosting, object>>>
            {
                jp => jp.JobAdvertiser
            };
            return await GetAsync(predicate: null, orderBy: null, includes: includes);
        }

        public async Task<JobPosting> GetByReferenceNumberAsync(string referenceNumber)
        {
            return (await GetAsync(jp => jp.ReferenceNumber == referenceNumber)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<JobPosting>> GetByCategoryIdAsync(int categoryId) 
        { 
            return await GetAsync(jp => jp.JobCategoryId == categoryId);
        }
    }
}
