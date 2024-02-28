using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Translate.Domain;

public static class DependencyInjecton
{
    public static IServiceCollection AddDependencyInjectionDomain(this IServiceCollection services)
    {
        AddMediatR(services);

        return services;
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}