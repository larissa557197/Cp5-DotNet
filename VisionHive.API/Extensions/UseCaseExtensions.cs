using VisionHive.Application.UseCases;

namespace VisionHive.API.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        // todos os casos de uso
        services.AddScoped<IMotoUseCase, MotoUseCase>();
        services.AddScoped<IFilialUseCase, FilialUseCase>();
        services.AddScoped<IPatioUseCase, PatioUseCase>();
    }
}