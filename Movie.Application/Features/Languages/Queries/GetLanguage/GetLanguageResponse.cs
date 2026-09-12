namespace Movie.Application.Features.Languages.Queries.GetLanguage
{
    public sealed class GetLanguageResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string NativeName { get; init; } = string.Empty;

        public string IsoCode { get; init; } = string.Empty;
    }
}
