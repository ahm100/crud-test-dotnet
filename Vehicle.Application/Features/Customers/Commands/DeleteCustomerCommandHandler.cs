using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Application.Common.Exceptions;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Domain.Entities;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;

        public DeleteCustomerCommandHandler(ICustomerRepository CustomerRepository, IMapper mapper, ILogger<DeleteCustomerCommandHandler> logger)
        {
            _customerRepository = CustomerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToDelete = await _customerRepository.GetByIdAsync(request.Id);
            if (customerToDelete == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            await _customerRepository.DeleteAsync(customerToDelete);
            _logger.LogInformation($"the {customerToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
