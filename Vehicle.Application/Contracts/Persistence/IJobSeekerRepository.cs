using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IJobSeekerRepository : IAsyncRepository<JobSeeker>
    {
        Task<IReadOnlyList<JobSeeker>> GetBySkillAsync(string skill);
        Task<IReadOnlyList<JobSeeker>> GetByBirthYearAsync(int year);
        Task<IReadOnlyList<JobSeeker>> GetOrderedByLastNameAsync();
        Task<IReadOnlyList<JobSeeker>> GetWithRelatedDataAsync();
        Task<JobSeeker> GetByEmailAsync(string email);
    }
}
