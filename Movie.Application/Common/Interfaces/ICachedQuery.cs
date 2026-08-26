using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface ICachedQuery<TResposnse>
        : IRequest<TResposnse>
    {
        string CacheKey { get; }

        TimeSpan? Expiration { get; }

        IEnumerable<string>? Tags { get; }
    }
}
