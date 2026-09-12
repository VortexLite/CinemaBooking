using MediatR;

namespace Movie.Application.Features.Languages.Commands.DeleteLanguage
{
    public sealed record DeleteLanguageCommand(Guid Id)
        : IRequest;
}
