using Movie.Domain.Common;
using Movie.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Language
{
    public sealed class Language : AggregateRoot
    {
        public string Name { get; private set; } = null!;
        public string NativeName { get; private set; } = null!;
        public string IsoCode { get; private set; } = null!;
        public ICollection<Movie.Movie> Movies { get; } =
            new List<Movie.Movie>();

        private Language() { }

        public Language(
            string name,
            string nativeName,
            string isoCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Language name is required.");

            if (string.IsNullOrWhiteSpace(nativeName))
                throw new DomainException("Native language name is required.");

            if (string.IsNullOrWhiteSpace(isoCode))
                throw new DomainException("ISO code is required.");

            Name = name.Trim();
            NativeName = nativeName.Trim();
            IsoCode = isoCode.ToUpper();
        }
    }
}
