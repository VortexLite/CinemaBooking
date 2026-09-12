using FluentValidation;

namespace Movie.Application.Features.Halls.Commands.DeleteHall
{
    public sealed class DeleteHallCommandValidator
        : AbstractValidator<DeleteHallCommand>
    {
        public DeleteHallCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
