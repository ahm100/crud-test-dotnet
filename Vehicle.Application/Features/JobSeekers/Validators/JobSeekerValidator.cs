using FluentValidation;
using PhoneNumbers;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Features.JobSeekers.Validators
{
    public class JobSeekerValidator : AbstractValidator<JobSeeker>
    {
        public JobSeekerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.PhoneNumber).NotEmpty().Must(BeAValidPhoneNumber);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.BankAccountNumber).NotEmpty();
            RuleFor(x => x.Skills).NotEmpty();
            RuleFor(x => x.Experience).NotEmpty();
        }

        private bool BeAValidPhoneNumber(string phoneNumber)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                //var numberProto = phoneUtil.Parse(phoneNumber, "US");
                var numberProto = phoneUtil.Parse(phoneNumber, null);
                return phoneUtil.IsValidNumber(numberProto);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}
