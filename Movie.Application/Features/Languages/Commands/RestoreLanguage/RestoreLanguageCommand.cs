using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.RestoreLanguage
{
    public sealed record RestoreLanguageCommand(Guid Id)
        : IRequest;
}
