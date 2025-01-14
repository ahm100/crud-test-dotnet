using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vehicle.Domain.Common;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<PaginatedList<T>> GetAllPaginatedWithCountAsync(int pageNumber = 1, int pageSize = 10);
        Task<PaginatedList<T>> GetPaginatedWithCountAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10);

        Task<IReadOnlyList<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10);
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
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string[] includes = null,
            bool disableTracking = true,
            int pageNumber = 1, int pageSize = 10);
        //Task<IReadOnlyList<T>> GetAsync(
        //    Expression<Func<T, bool>> predicate = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    List<Expression<Func<T, object>>> includes = null,
        //    bool disableTracking = true,
        //    int pageNumber = 1, int pageSize = 10);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
