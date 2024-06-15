using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }

    }
}
