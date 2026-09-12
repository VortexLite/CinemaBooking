using FluentValidation;

namespace Movie.Application.Features.Halls.Commands.UpdateHall
{
    public sealed class UpdateHallValidator
        : AbstractValidator<UpdateHallCommand>
    {
        public UpdateHallValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Rows)
                .GreaterThan((ushort)0)
                .LessThanOrEqualTo((ushort)100);

            RuleFor(x => x.Columns)
                .GreaterThan((ushort)0)
                .LessThanOrEqualTo((ushort)100);
        }
    }
}
