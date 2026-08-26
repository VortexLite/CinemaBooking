using Movie.Api.DependencyInjection;
using Movie.Api.Extensions;
using Movie.Application.DependencyInjection;
using Movie.Infrastructure.DependencyInjection;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Seed;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddExtensions();

builder.AddInfrastructureAspire();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandling();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();

    await DatabaseInitializer.InitializeAsync(app.Services);
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
