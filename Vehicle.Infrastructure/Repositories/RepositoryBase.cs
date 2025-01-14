using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Common;
using Vehicle.Infrastructure.Persistence;

namespace Vehicle.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await ApplyPagination(_dbContext.Set<T>(), pageNumber, pageSize).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10)
        {
            return await ApplyPagination(_dbContext.Set<T>().Where(predicate), pageNumber, pageSize).ToListAsync();
        }

        /*
             var products = await repository.GetAsync(
                predicate: p => p.Category.Name == "Electronics",  // Filter products by category name
                includes: new string[] { "Category", "Supplier" },  // Include Category and Supplier in the query
                orderBy: products => products.OrderBy(p => p.Name),  // Order by product name
                disableTracking: true,
                pageNumber: 1,
                pageSize: 10
            );

         */
        public async Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string[] includes = null,
            bool disableTracking = true,
            int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return await ApplyPagination(query, pageNumber, pageSize).ToListAsync();
        }

        //public async Task<IReadOnlyList<T>> GetAsync(
        //    Expression<Func<T, bool>> predicate = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    List<Expression<Func<T, object>>> includes = null,
        //    bool disableTracking = true,
        //    int pageNumber = 1, int pageSize = 10)
        //{
        //    IQueryable<T> query = _dbContext.Set<T>();
        //    if (disableTracking) query = query.AsNoTracking();

        //    if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null) query = orderBy(query);

        //    return await ApplyPagination(query, pageNumber, pageSize).ToListAsync();
        //}

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplyPagination(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public async Task<PaginatedList<T>> GetAllPaginatedWithCountAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await CreatePaginatedListWithCountAsync(_dbContext.Set<T>(), pageNumber, pageSize);
        }

        public async Task<PaginatedList<T>> GetPaginatedWithCountAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10)
        {
            return await CreatePaginatedListWithCountAsync(_dbContext.Set<T>().Where(predicate), pageNumber, pageSize);
        }



        private async Task<PaginatedList<T>> CreatePaginatedListWithCountAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
