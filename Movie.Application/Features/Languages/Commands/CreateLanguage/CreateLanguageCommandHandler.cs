using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Language;
using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.CreateLanguage
{
    public sealed class CreateLanguageCommandHandler
        : IRequestHandler<CreateLanguageCommand, Guid>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLanguageCommandHandler(
            ILanguageRepository languageRepository, 
            IUnitOfWork unitOfWork)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateLanguageCommand request,
            CancellationToken cancellationToken)
        {
            if (await _languageRepository.ExistsByNameAsync(request.Name,
                cancellationToken))
                throw new DomainException($"Language '{request.Name}' already exists.");

            if (await _languageRepository.ExistsByIsoCodeAsync(request.IsoCode, 
                cancellationToken))
                throw new DomainException($"Language with ISO code '{request.IsoCode}' already exists.");

            var language = new Language(
                request.Name,
                request.NativeName,
                request.IsoCode);

            await _languageRepository.AddAsync(language, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return language.Id;
        }
    }
}
