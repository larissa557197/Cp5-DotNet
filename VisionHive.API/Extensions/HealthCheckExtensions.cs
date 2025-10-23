using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using VisionHive.Application.Configs;

namespace VisionHive.API.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddChecks(this IServiceCollection services, Settings settings)
    {
        // Cria o cliente Mongo para ser reutilizado pelo HealthCheck
        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(settings.MongoDb.ConnectionString));

        services
            .AddHealthChecks()
            // Checa conexão com o Oracle (v1 da API)
            .AddOracle(
                settings.ConnectionStrings.DefaultConnection,
                name: "Oracle")
            // Checa o MongoDB (v2 da API)
            .AddMongoDb(
                settings.MongoDb.ConnectionString, // 1º parâmetro: string de Conexão
                settings.MongoDb.DatabaseName,    // 2º parâmetro: Nome do Banco de Dados
                name: "MongoDB")
            // URLs externas
            .AddUrlGroup(new Uri("https://fiap.com.br"), name: "FIAP")
            .AddUrlGroup(new Uri("https://google.com.br"), name: "Google");

        return services;
    }

    public static Task WriteResponse(HttpContext context, HealthReport report)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(new
        {
            Status = report.Status.ToString(),
            Duration = report.TotalDuration,
            Info = report.Entries.Select(entry => new
            {
                entry.Key,
                entry.Value.Description,
                entry.Value.Duration,
                Status = Enum.GetName(typeof(HealthStatus), entry.Value.Status),
                Error = entry.Value.Exception?.Message,
                entry.Value.Data
            }).ToList()
        }, jsonSerializerOptions);

        context.Response.ContentType = MediaTypeNames.Application.Json;
        return context.Response.WriteAsync(json);
    }
}