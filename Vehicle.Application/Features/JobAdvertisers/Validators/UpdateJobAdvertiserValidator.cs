using FluentValidation;
using PhoneNumbers;

namespace Vehicle.Application.Features.JobAdvertisers.Commands
{
    public class UpdateJobAdvertiserValidator : AbstractValidator<UpdateJobAdvertiserCommand>
    {
        public UpdateJobAdvertiserValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            //RuleFor(x => x.Description).NotEmpty();
            //RuleFor(x => x.WebsiteUrl).NotEmpty().Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute));
            //RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
            //RuleFor(x => x.ContactPhoneNumber).NotEmpty().Must(BeAValidPhoneNumber);
        }

        private bool BeAValidPhoneNumber(string phoneNumber)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var numberProto = phoneUtil.Parse(phoneNumber, "US");
                return phoneUtil.IsValidNumber(numberProto);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}
