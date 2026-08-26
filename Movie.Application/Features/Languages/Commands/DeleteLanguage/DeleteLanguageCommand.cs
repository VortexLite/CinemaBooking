using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.DeleteLanguage
{
    public sealed record DeleteLanguageCommand(Guid Id)
        : IRequest;
}
