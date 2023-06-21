using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectManagement.Infra;

public static class EFConfiguration
{
    public static IServiceCollection ConfigureEF(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("default")!;

        services
            .AddPooledDbContextFactory<DatabaseContext>(o => o.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceProvider MigrateDatabase(this IServiceProvider provider)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
            using (var scope = provider.CreateScope())
            {
                var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DatabaseContext>>();
                var dbContext = dbContextFactory.CreateDbContext();
                dbContext.Database.Migrate();
                DatabaseSeeder.SeedData(dbContext);
            }

        return provider;
    }

    private static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        return services;
    }
}
