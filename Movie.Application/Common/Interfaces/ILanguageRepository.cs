using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface ILanguageRepository
        : IBaseRepository<Language>
    {
        Task<List<Language>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Language?> GetByIsoCodeAsync(string isoCode,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<Language?> GetByIdIncludingDeletedAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByIsoCodeAsync(string isoCode,
            CancellationToken cancellationToken = default);
    }
}
