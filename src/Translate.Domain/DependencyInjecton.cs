using Microsoft.Extensions.DependencyInjection;
using Translate.Domain.Handlers.CriarFrase;

namespace Translate.Domain;

public static class DependencyInjecton
{
    public static IServiceCollection AddDependencyInjectionDomain(this IServiceCollection services)
    {
        AddHandlers(services);

        return services;
    }

    private static void AddHandlers(IServiceCollection services)
    {
        services.AddTransient<ICriarFraseHandler, CriarFraseHandler>();
    }
}