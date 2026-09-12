using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Language;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class LanguageRepository
        : BaseRepository<Language>,
        ILanguageRepository
    {
        public LanguageRepository(MovieDbContext context)
            : base(context) { }


        public async Task<List<Language>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Language?> GetByIsoCodeAsync(
            string isoCode, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(x => x.IsoCode == isoCode, 
                cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .AnyAsync(x => x.Id == id, 
                cancellationToken);
        }

        public async Task<Language?> GetByIdIncludingDeletedAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, 
            CancellationToken cancellationToken = default)
        {
            string normalizedName = name.Trim();

            return await _context.Languages
                .IgnoreQueryFilters()
                .AnyAsync(x => EF.Functions.ILike(
                    x.Name,
                    normalizedName),
                    cancellationToken);
        }

        public Task<bool> ExistsByIsoCodeAsync(string isoCode, 
            CancellationToken cancellationToken = default)
        {
            string normalizedIsoCode = isoCode.Trim().ToUpperInvariant();

            return _context.Languages
                .IgnoreQueryFilters()
                .AnyAsync(x => x.IsoCode == normalizedIsoCode,
                    cancellationToken);
        }
    }
}
