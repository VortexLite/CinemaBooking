using Mapster;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Languages.Queries.GetLanguage;
using Movie.Application.Features.Languages.Queries.GetLanguages;

namespace Movie.Infrastructure.Persistence.Queries
{
    public sealed class LanguageQueries
        : BaseQuery, ILanguageQueries
    {
        public LanguageQueries(MovieDbContext context) : base(context) { }

        public async Task<GetLanguageResponse?> GetLanguageAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<GetLanguageResponse>()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<GetLanguagesResponse>> GetLanguagesAsync(bool includeDeleted, 
            CancellationToken cancellationToken = default)
        {
            var query = _context.Languages.AsNoTracking();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();
            
            return query
                .OrderBy(x => x.Name)
                .ProjectToType<GetLanguagesResponse>()
                .ToListAsync(cancellationToken);
        }
    }
}
