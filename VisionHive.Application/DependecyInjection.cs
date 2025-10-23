using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using VisionHive.Domain.Repositories;
using VisionHive.Infrastructure.Contexts;
using VisionHive.Application.Configs;
using VisionHive.Infrastructure.Repositories;
using VisionHive.Infrastructure.Repositories.Mongo;

//using VisionHive.Infrastructure.Mongo.Repositories;


namespace VisionHive.Application;

public static class DependecyInjection
{
    private static IServiceCollection AddDBContext(this IServiceCollection services, Settings settings)
    {
        // Conexão com Oracle (CP4)
        return services.AddDbContext<VisionHiveContext>(options =>
        {
            options.UseOracle(settings.ConnectionStrings.DefaultConnection);
        });
    }

    private static IServiceCollection AddMongoContext(this IServiceCollection services, Settings settings)
    {
        // Registra MongoClient e IMongoDatabase com ciclo de vida correto
        services.AddSingleton<IMongoClient>(_ => new MongoClient(settings.MongoDb.ConnectionString));
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.MongoDb.DatabaseName);
        });

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        // Repositórios relacionais (Oracle)
        services.AddScoped<IMotoRepository, MotoRepository>();
        services.AddScoped<IFilialRepository, FilialRepository>();
        services.AddScoped<IPatioRepository, PatioRepository>();

        // Se existir repositório Mongo:
        services.AddScoped<FilialMongoRepository>();
        services.AddScoped<PatioMongoRepository>();

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