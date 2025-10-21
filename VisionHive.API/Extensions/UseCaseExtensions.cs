using VisionHive.Application.UseCases;

namespace VisionHive.API.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        // todos os casos de uso
        services.AddScoped<IMotoUseCase, MotoUseCase>();
        services.AddScoped<IPatioUseCase, PatioUseCase>();
        services.AddScoped<IFilialUseCase, FilialUseCase>();

        return services;


    }
}