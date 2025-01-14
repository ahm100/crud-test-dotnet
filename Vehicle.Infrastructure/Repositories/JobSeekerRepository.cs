using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Infrastructure.Persistence;

namespace Vehicle.Infrastructure.Repositories
{
    public class JobSeekerRepository : RepositoryBase<JobSeeker>, IJobSeekerRepository
    {
        public JobSeekerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<JobSeeker>> GetBySkillAsync(string skill, int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(js => js.Skills.Any(s => s.SkillName == skill), pageNumber: pageNumber, pageSize: pageSize);
        }

        public async Task<IReadOnlyList<JobSeeker>> GetByBirthYearAsync(int year, int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(js => js.DateOfBirth.Year == year, pageNumber: pageNumber, pageSize: pageSize);
        }

        public async Task<IReadOnlyList<JobSeeker>> GetOrderedByLastNameAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(
                predicate: null, 
                orderBy: query => query.OrderBy(js => js.LastName), 
                includes: new string[] { },  //[posts, advertiser,...] could include any relaed table
                disableTracking: true, pageNumber: pageNumber, pageSize: pageSize);
        }

     
        public async Task<IReadOnlyList<JobSeeker>> GetWithRelatedDataAsync(int pageNumber = 1, int pageSize = 10)
        {
            var includes = new string[] { "Skills", "Experience" };
            return await GetAsync(
                predicate: null,
                orderBy: null,
                includes: includes,
                disableTracking: true,
                pageNumber: pageNumber,
                pageSize: pageSize
            );
        }

        public async Task<JobSeeker> GetByEmailAsync(string email)
        {
            var jobSeekers = await GetAsync(js => js.Email == email, orderBy: null,
                includes: new string[] { },  //[posts, advertiser,...] could include any relaed table
                disableTracking: true);
            return jobSeekers.FirstOrDefault();
        }
    }
}
