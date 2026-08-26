using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.DeleteLanguage
{
    public sealed class DeleteLanguageCommandHandler
        : IRequestHandler<DeleteLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLanguageCommandHandler(
            ILanguageRepository languageRepository, 
            IUnitOfWork unitOfWork)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteLanguageCommand request, 
            CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetByIdAsync(request.Id,
                cancellationToken);

            if (language is null)
                throw new NotFoundException(nameof(Domain.Entities.Language.Language),
                request.Id);

            language.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
