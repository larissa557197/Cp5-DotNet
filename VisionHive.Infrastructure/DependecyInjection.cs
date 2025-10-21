using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisionHive.API.Configs;
using VisionHive.Infrastructure.Contexts;
using MongoDB.Driver;
using VisionHive.Domain.Repositories;
using VisionHive.Infrastructure.Mongo.Repositories;

namespace VisionHive.Application;

public static class DependecyInjection
{
    private static IServiceCollection AddDBContext(this IServiceCollection services, Settings settings)
    {
        return services.AddDbContext<VisionHiveContext>(options =>
            options.UseOracle(settings.ConnectionStrings.DefaultConnection));
    }

    private static IServiceCollection AddMongoContext(this IServiceCollection services, Settings settings)
    {
        var client = new MongoClient(settings.MongoDb.ConnectionString);
        var database = client.GetDatabase(settings.MongoDb.DatabaseName);
        services.AddSingleton<IMongoDatabase>(database);
        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IMotoRepository, MotoRepository>();
        services.AddScoped<IFilialRepository, FilialRepository>();
        services.AddScoped<IPatioRepository, PatioRepository>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Settings settings)
    {
        AddDBContext(services, settings);
        AddMongoContext(services, settings);
        AddRepository(services);
        return services;
    }
}
