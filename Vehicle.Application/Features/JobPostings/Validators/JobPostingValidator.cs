using FluentValidation;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Features.JobPostings.Validators
{
    public class JobPostingValidator : AbstractValidator<JobPosting>
    {
        public JobPostingValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ReferenceNumber).NotEmpty();
            RuleFor(x => x.PostDate).NotEmpty().LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.ExpiryDate).GreaterThan(x => x.PostDate);
            RuleFor(x => x.Salary).GreaterThan(0);
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.JobAdvertiserId).NotEmpty();
            RuleFor(x => x.JobCategoryId).NotEmpty();
        }
    }
}
