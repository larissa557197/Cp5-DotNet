using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using VisionHive.API.Extensions;
using VisionHive.Application;
using VisionHive.Application.Configs;

namespace VisionHive.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // le as configurações do appsettings.json
        var settings = builder.Configuration.Get<Settings>();

        // controllers
        builder.Services.AddControllers();
        
        // swagger + API Explorer
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger(settings.Swagger);
        
        // Mongo
        builder.Services.AddSingleton(settings.MongoDb);

        // infrastructure (Oracle + Mongo + Repositórios)
        builder.Services.AddInfrastructure(settings);
        
        // usecases (camada application)
        builder.Services.AddUseCase();
        
        // Helth Checks (Oracle, Mongo, URLs)
        builder.Services.AddChecks(settings);
        
        // versionamento da api
        builder.Services.AddApiVersioning();
        
        
        var app = builder.Build();

        // swagger (dev)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                ui.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });
        }
        
        app.UseRouting();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health-check", new HealthCheckOptions()
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse
            });
        });

        app.Run();
    }
}
