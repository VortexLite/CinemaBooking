using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.CreateLanguage
{
    public sealed record CreateLanguageCommand(
        string Name,
        string NativeName,
        string IsoCode)
        : IRequest<Guid>;
}
