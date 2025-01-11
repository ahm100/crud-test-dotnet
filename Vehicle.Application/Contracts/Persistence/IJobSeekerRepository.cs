using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IJobSeekerRepository : IAsyncRepository<JobSeeker>
    {
        Task<IReadOnlyList<JobSeeker>> GetBySkillAsync(string skill, int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobSeeker>> GetByBirthYearAsync(int year, int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobSeeker>> GetOrderedByLastNameAsync(int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<JobSeeker>> GetWithRelatedDataAsync(int pageNumber = 1, int pageSize = 10);
        Task<JobSeeker> GetByEmailAsync(string email);
    }
}
