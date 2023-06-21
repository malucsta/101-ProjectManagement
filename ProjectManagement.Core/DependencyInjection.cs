using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Core.People.Services;

namespace ProjectManagement.Core;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}
