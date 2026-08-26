using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Queries.GetLanguages
{
    public sealed class GetLanguagesResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string NativeName { get; init; } = string.Empty;

        public string IsoCode { get; init; } = string.Empty;
    }
}
