using Microsoft.OpenApi.Models;
using VisionHive.Application.Configs;

namespace VisionHive.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSettings settings)
    {
        return services.AddSwaggerGen(swagger =>
        {
            // v1
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = settings.Title,
                Version = "v1",
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name  = settings.Contact?.Name,
                    Email = settings.Contact?.Email
                }
            });

            // v2
            swagger.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = $"{settings.Title} v2",
                Version = "v2",
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name  = settings.Contact?.Name,
                    Email = settings.Contact?.Email
                }
            });

            // servers do appsettings
            if (settings.Servers is not null)
            {
                foreach (var server in settings.Servers)
                {
                    swagger.AddServer(new OpenApiServer
                    {
                        Url = server.Url,
                        Description = server.Description
                    });
                }
            }
        });
    }
}