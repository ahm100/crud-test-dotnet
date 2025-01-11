using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IJobPostingRepository : IAsyncRepository<JobPosting>
    {
        Task<IReadOnlyList<JobPosting>> GetByTitleAsync(string title);
        Task<IReadOnlyList<JobPosting>> GetOrderedByDatePostedAsync();
        Task<IReadOnlyList<JobPosting>> GetWithAdvertiserAsync();
        Task<JobPosting> GetByReferenceNumberAsync(string referenceNumber);
        Task<IReadOnlyList<JobPosting>> GetByCategoryIdAsync(int categoryId);

    }
}
