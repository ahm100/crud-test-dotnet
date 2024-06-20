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
using Azure.Core;
using PhoneNumbers;

namespace TestProject
{
    public class MobileValidatorTest
    {
        [Fact]
        public async Task ValidateInputNumber()
        {
            // Arrange
            var inputedNumber = "+982188776655";
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var numberProto = phoneUtil.Parse(inputedNumber, "IR");
            var formattedPhoneNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.E164);



            // Act
            bool isValid = phoneUtil.IsValidNumber(numberProto);
            if (!phoneUtil.IsValidNumber(numberProto))
            {
                throw new ValidationException("Invalid phone number");
            }

            if (formattedPhoneNumber.ToString().TrimStart().StartsWith("+9891")) // it is a valid cell phone in Iran
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }


            // Assert
            //Assert.True(result >= 0); // Verify that the result is a positive integer
            Assert.False(isValid);
        }
    }

  
}
