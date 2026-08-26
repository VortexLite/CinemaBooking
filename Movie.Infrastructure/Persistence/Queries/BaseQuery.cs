using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Queries
{
    public abstract class BaseQuery
    {
        protected readonly MovieDbContext _context;

        protected BaseQuery(MovieDbContext context)
        {
            _context = context;
        }
    }
}
