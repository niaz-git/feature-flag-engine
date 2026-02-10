
using FeatureFlags.Application.Interfaces;
using FeatureFlags.Application.Services;
using FeatureFlags.Domain.Engine;
using FeatureFlags.Infrastructure.Persistence;
using FeatureFlags.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace FeatureFlags.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<FeatureFlagDbContext>(options =>
            {

                var dbPath = Path.Combine(
           AppContext.BaseDirectory,
               "featureflags.db");

                options.UseSqlite($"Data Source=featureflags.db");
            }
           
            );
            builder.Services.AddScoped<IFeatureMutationService,FeatureMutationService>();
            builder.Services.AddScoped<IFeatureEvaluationService, FeatureEvaluationService>();

            builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
            builder.Services.AddScoped<IOverrideRepository, OverrideRepository>();
            builder.Services.AddScoped<IFeatureFlagEngine, FeatureFlagEngine>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
