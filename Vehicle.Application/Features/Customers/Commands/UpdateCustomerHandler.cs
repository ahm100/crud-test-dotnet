using MediatR;
using FluentValidation;
//using CrudTest.Application.Common.Interfaces;
//using CrudTest.Domain.Entities;
using PhoneNumbers;
//using CrudTest.Domain.Entities;
//using CrudTest.Application.Commands;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Common.Exceptions;
using System.Runtime.InteropServices;
using AutoMapper;

namespace CrudTest.Application.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _context;
        private readonly IValidator<Customer> _validator;
        private readonly ILogger<UpdateCustomerHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(ICustomerRepository context, IValidator<Customer> validator, IMapper mapper, ILogger<UpdateCustomerHandler> logger)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToUpdate = await _context.GetByIdAsync(request.Id);

            if (customerToUpdate == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            var phoneUtil = PhoneNumberUtil.GetInstance();
            var numberProto = phoneUtil.Parse(request.PhoneNumber, "US");
            var formattedPhoneNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.E164);

            if (!phoneUtil.IsValidNumber(numberProto))
            {
                throw new ValidationException("Invalid phone number");
            }

            if (request.Email != customerToUpdate.Email)
            {
                if (await _context.findBYEmail(request.Email) != null)
                {
                    throw new ValidationException("Email must be unique");
                }
            }

            //with automapper automatically ignores not sent fields naturally
            _mapper.Map(request, customerToUpdate, typeof(UpdateCustomerCommand), typeof(Customer));

            await _context.UpdateAsync(customerToUpdate);
            _logger.LogInformation($"{request.Id}");

            return Unit.Value;
        }
    }
}
