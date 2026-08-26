using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.RestoreLanguage
{
    public sealed class RestoreLanguageCommandHandler
        : IRequestHandler<RestoreLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreLanguageCommandHandler(
            ILanguageRepository languageRepository, 
            IUnitOfWork unitOfWork)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RestoreLanguageCommand request, 
            CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetByIdIncludingDeletedAsync(
                request.Id, cancellationToken);

            if (language is null)
                throw new NotFoundException(nameof(Domain.Entities.Language.Language),
                    request.Id);

            language.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
