
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IJobPostingRepository : IAsyncRepository<JobPosting>
    {
        Task<IReadOnlyList<JobPosting>> GetByTitleAsync(string title, int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobPosting>> GetOrderedByDatePostedAsync(int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobPosting>> GetWithAdvertiserAsync(int pageNumber = 1, int pageSize = 10);
        Task<JobPosting> GetByReferenceNumberAsync(string referenceNumber);
        Task<IReadOnlyList<JobPosting>> GetByCategoryIdAsync(int categoryId, int pageNumber = 1, int pageSize = 10);
    }
}
