using FluentValidation;

namespace TorcAssestmentAPI.Models
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50);

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position name is required")
                .MaximumLength(50);

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage("Salary must be greater than zero");

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Hire date cannot be in the future");
        }
    }
}
