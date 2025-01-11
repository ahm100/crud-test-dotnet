using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IJobAdvertiserRepository : IAsyncRepository<JobAdvertiser>
    {
        Task<IReadOnlyList<JobAdvertiser>> GetByCompanyNameAsync(string companyName, int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobAdvertiser>> GetOrderedByContactEmailAsync(int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobAdvertiser>> GetWithPostedJobsAsync(int pageNumber = 1, int pageSize = 10);
        Task<JobAdvertiser> GetByContactEmailAsync(string email);
    }
}
