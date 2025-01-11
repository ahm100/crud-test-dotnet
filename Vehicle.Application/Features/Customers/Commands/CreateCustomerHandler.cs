//using MediatR;
//using FluentValidation;
//using PhoneNumbers;
//using Vehicle.Application.Features.Customers.Commands;
//using Vehicle.Domain.Entities.Concrete;
//using Vehicle.Application.Contracts.Persistence;
//using Microsoft.Extensions.Logging;

//namespace CrudTest.Application.Handlers
//{
//    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
//    {
//        private readonly ICustomerRepository _context;
//        private readonly IValidator<Customer> _validator;
//        private readonly ILogger<CreateCustomerHandler> _logger;

//        public CreateCustomerHandler(ICustomerRepository context, IValidator<Customer> validator, ILogger<CreateCustomerHandler> logger)
//        {
//            _context = context;
//            _validator = validator;
//            _logger = logger;
//        }

//        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
//        {
//            var phoneUtil = PhoneNumberUtil.GetInstance();
//            var numberProto = phoneUtil.Parse(request.PhoneNumber, "US");
//            var formattedPhoneNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.E164);

//            // We can use automapper instead like bellow
//            // customerToAdd = new Customer();
//            //_mapper.Map(request, customerToAdd, typeof(CreateCustomerCommand), typeof(Customer));

//            var customer = new Customer
//            {
//                FirstName = request.FirstName,
//                LastName = request.LastName,
//                DateOfBirth = request.DateOfBirth,
//                PhoneNumber = formattedPhoneNumber,
//                Email = request.Email,
//                BankAccountNumber = request.BankAccountNumber
//            };

//            var validationResult = _validator.Validate(customer);
//            if (!validationResult.IsValid)
//            {
//                throw new ValidationException(validationResult.Errors);
//            }

//            // Uniqueness checks
//            if (await _context.findWithFLD(customer.FirstName, customer.LastName, customer.DateOfBirth)!= null)
//            {
//                throw new ValidationException("Customer already exists.");
//            }
//            if (await _context.findBYEmail(customer.Email)!= null)
//            {
//                throw new ValidationException("Email already exists.");
//            }


//            var result = await _context.AddAsync(customer);
//            _logger.LogInformation($"{result.Id}");
//            return result.Id;
//        }
//    }
//}

using MediatR;
using FluentValidation;
using PhoneNumbers;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace CrudTest.Application.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _context;
        private readonly IValidator<Customer> _validator;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(ICustomerRepository context, IValidator<Customer> validator, ILogger<CreateCustomerHandler> logger, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var numberProto = phoneUtil.Parse(request.PhoneNumber, "US");
            var formattedPhoneNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.E164);

            var customer = _mapper.Map<Customer>(request);
            customer.PhoneNumber = formattedPhoneNumber;

            var validationResult = _validator.Validate(customer);
            if (!validationResult.IsValid)
            {
                throw new CustomValidationException(validationResult.Errors);
            }

            if (await _context.findWithFLD(customer.FirstName, customer.LastName, customer.DateOfBirth) != null)
            {
                throw new CustomValidationException("Customer already exists.");
            }
            if (await _context.findBYEmail(customer.Email) != null)
            {
                throw new CustomValidationException("Email already exists.");
            }

            var result = await _context.AddAsync(customer);
            _logger.LogInformation($"{result.Id}");
            return result.Id;
        }
    }

    public class CustomValidationException : Exception
    {
        public CustomValidationException(IEnumerable<FluentValidation.Results.ValidationFailure> errors)
            : base(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage))) { }

        public CustomValidationException(string message) : base(message) { }
    }
}
