using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;

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

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .AnyAsync(x => x.Id == id, 
                cancellationToken);
        }
    }
}
