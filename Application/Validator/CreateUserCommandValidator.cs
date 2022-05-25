using Application.Commands.UserCommand;
using FluentValidation;

namespace Application.Validator
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().MaximumLength(200).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}