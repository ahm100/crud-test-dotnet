using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IJobAdvertiserRepository : IAsyncRepository<JobAdvertiser>
    {
        Task<IReadOnlyList<JobAdvertiser>> GetByCompanyNameAsync(string companyName);
        Task<IReadOnlyList<JobAdvertiser>> GetOrderedByContactEmailAsync();
        Task<IReadOnlyList<JobAdvertiser>> GetWithPostedJobsAsync();
        Task<JobAdvertiser> GetByContactEmailAsync(string email);
    }
}
