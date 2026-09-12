using FluentValidation;

namespace Movie.Application.Features.Sessions.Queries.GetSessions
{
    public sealed class GetSessionsQueryValidator
    : AbstractValidator<GetSessionsQuery>
    {
        public GetSessionsQueryValidator()
        {
            RuleFor(x => x)
                .Must(x =>
                    !x.FromUtc.HasValue ||
                    !x.ToUtc.HasValue ||
                    x.FromUtc.Value < x.ToUtc.Value)
                .WithMessage("FromUtc must be earlier than ToUtc.");
        }
    }
}
