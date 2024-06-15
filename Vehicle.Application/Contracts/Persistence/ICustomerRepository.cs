using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
        Task<Customer?> findWithFLD(string firstName, string lastName,DateTime dateOfBirth);
        Task<Customer?> findBYEmail(string email);
    }
}
