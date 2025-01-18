

using FluentValidation;
using Vehicle.Application.Features.JobSeekers.Commands;
using Vehicle.Application.Features.JobSeekers.Validators;

public class CreateJobSeekerCommandValidator : AbstractValidator<CreateJobSeekerCommand>
{
    public CreateJobSeekerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
        // Add other validation rules as needed
    }
}
