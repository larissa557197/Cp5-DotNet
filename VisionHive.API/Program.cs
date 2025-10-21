using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using VisionHive.API.Configs;
using VisionHive.API.Extensions;
using VisionHive.Application;

namespace VisionHive.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var settings = builder.Configuration.Get<Settings>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger(settings.Swagger);
        builder.Services.AddChecks(settings);
        builder.Services.AddInfrastructure(settings);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        });

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
