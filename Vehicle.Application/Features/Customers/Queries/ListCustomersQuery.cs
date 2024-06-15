using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application.Features.Customers.Queries
{
    public class ListCustomersQuery : IRequest<List<CustomerVM>>
    {
       
    }
}
