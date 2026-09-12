using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Language;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Languages.Commands.UpdateLanguage
{
    public sealed class UpdateLanguageCommandHandler
        : IRequestHandler<UpdateLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLanguageCommandHandler(
            ILanguageRepository languageRepository, 
            IUnitOfWork unitOfWork)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateLanguageCommand request, 
            CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetByIdAsync(request.Id,
                cancellationToken);

            if (language is null)
                throw new NotFoundException(nameof(Language), request.Id);

            var nameExists = await _languageRepository.ExistsByNameAsync(request.Name, cancellationToken);

            if (nameExists && !string.Equals(
                language.Name,
                request.Name.Trim(),
                StringComparison.OrdinalIgnoreCase))
                throw new BusinessRuleException($"Language '{request.Name}' already exists.");

            var normalizedIso = request.IsoCode.Trim().ToUpperInvariant();

            var isoExists = await _languageRepository.ExistsByIsoCodeAsync(normalizedIso,
                cancellationToken);

            if (isoExists && language.IsoCode != normalizedIso)
                throw new BusinessRuleException($"Language with ISO code '{request.IsoCode}' already exists.");

            language.Update(request.Name,
                request.NativeName,
                request.IsoCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
