namespace Movie.Api.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddSerilogConfiguration(
            this WebApplicationBuilder builder)
        {

            return builder;
        }
    }
}
