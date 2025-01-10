using System;
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

        public async Task<IReadOnlyList<JobSeeker>> GetBySkillAsync(string skill)
        {
            return await GetAsync(js => js.Skills.Any(s => s.Skill == skill));
        }

        public async Task<IReadOnlyList<JobSeeker>> GetByBirthYearAsync(int year)
        {
            return await GetAsync(js => js.DateOfBirth.Year < year);
        }

        public async Task<IReadOnlyList<JobSeeker>> GetOrderedByLastNameAsync()
        {
            return await GetAsync(predicate: null, orderBy: query => query.OrderBy(js => js.LastName), includes: null);
        }


        public async Task<IReadOnlyList<JobSeeker>> GetWithRelatedDataAsync()
        {
            var includes = new List<Expression<Func<JobSeeker, object>>>
            {
                js => js.Skills,
                js => js.Experience
            };
            return await GetAsync(predicate: null, orderBy: null, includes: includes);
        }

        public async Task<JobSeeker> GetByEmailAsync(string email)
        {
            return (await GetAsync(js => js.Email == email)).FirstOrDefault();
        }
    }
}
