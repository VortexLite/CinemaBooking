using MediatR;

namespace Movie.Application.Features.Languages.Commands.RestoreLanguage
{
    public sealed record RestoreLanguageCommand(Guid Id)
        : IRequest;
}
