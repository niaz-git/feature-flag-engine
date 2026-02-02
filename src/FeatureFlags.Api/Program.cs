using FeatureFlags.Application.Interfaces;
using FeatureFlags.Application.Services;
using FeatureFlags.Domain.Engine;
using FeatureFlags.Infrastructure.Persistence;
using FeatureFlags.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<FeatureFlagDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("FeatureFlagsDb")
        ?? "Data Source=featureflags.db");
});

builder.Services.AddSingleton<IFeatureFlagEngine, FeatureFlagEngine>();


builder.Services.AddScoped<FeatureEvaluationService>();
builder.Services.AddScoped<FeatureMutationService>();

builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IOverrideRepository, OverrideRepository>();



var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ Enable controllers
app.MapControllers();

app.Run();