using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentValidation;
using FluentValidation.Results;
using CrudTest.Application.Handlers;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Domain.Entities.Concrete;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Contracts.Persistence;
using Vehicle.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Vehicle.Infrastructure.Persistence;

namespace TestProject
{
    public class CrudCustomerTests
    {
        [Fact]
        public async Task CreateCustomer()
        {
            // Arrange

            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //.UseInMemoryDatabase(databaseName: "TestDatabase") // Provide a unique name
            //.Options;
            //using var dbContext = new ApplicationDbContext(options); // Create an in-memory context
            //var customerRepository = new CustomerRepository(dbContext);

            var validatorMock = new Mock<IValidator<Customer>>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var customerValidator = new Mock<IValidator<Customer>>();
            var loggerMock = new Mock<ILogger<CreateCustomerHandler>>();

            // Create a mock logger
            validatorMock.Setup(v => v.Validate(It.IsAny<Customer>())).Returns(new ValidationResult());

            // Set up customer repository behavior
            mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(new CustomerResult { Id = 1 });

            //It.IsAny<Customer>(): This expression represents any instance of the Customer class. When you use it in a Moq setup, it allows the mocked method to accept any Customer object as an argument.

            var handler = new CreateCustomerHandler(mockCustomerRepository.Object, validatorMock.Object, loggerMock.Object);

            var command = new CreateCustomerCommand
            {
                FirstName = "mah",
                LastName = "hasti",
                DateOfBirth = new DateTime(2023, 1, 1),
                PhoneNumber = "+1234567893",
                Email = "a@example.com",
                BankAccountNumber = "123456789"
            };


            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result >= 0); // Verify that the result is a positive integer
        }
    }

    internal class CustomerResult : Customer
    {
        public int Id { get; set; }
    }
}
