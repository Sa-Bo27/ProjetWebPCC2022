using Application.Commands.UserCommand;
using FluentValidation;

namespace Application.Validator
{
    public class UpdateUserMovieListCommandValidator: AbstractValidator<UpdateUserMovieListCommand>
    {
        public UpdateUserMovieListCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.MovieDto).NotEmpty().NotNull();
        }
    }
}