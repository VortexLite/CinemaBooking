using MediatR;

namespace Movie.Application.Features.Languages.Commands.UpdateLanguage
{
    public sealed record UpdateLanguageCommand(
        Guid Id,
        string Name,
        string NativeName,
        string IsoCode)
        : IRequest;
}
