using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Real.Models;

namespace Real.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var useLocalDatabase = configuration.GetValue<bool>("UseLocalDatabase");

        if (useLocalDatabase)
        {
            services.AddSqliteDbServices(configuration);
        }
        else
        {
            services.AddSqlServerDbServices(configuration);
        }

        services.AddScoped<ApuracaoService>();
        services.AddScoped<FinancasInteligentesProcuder>();

        return services;
    }

    public static void EnsureDatabaseExists(this IServiceProvider services, IConfiguration configuration)
    {
        var useLocalDatabase = configuration.GetValue<bool>("UseLocalDatabase");

        if (useLocalDatabase)
        {
            services.EnsureSqliteDatabaseExists();
        }
        else
        {
            services.EnsureSqlServerDatabaseExists();
        }
    }

    public static async Task EnsureDatabaseExistsAsync(this IServiceProvider services, IConfiguration configuration)
    {
        var useLocalDatabase = configuration.GetValue<bool>("UseLocalDatabase");

        if (useLocalDatabase)
        {
            await services.EnsureSqliteDatabaseExistsAsync();
        }
        else
        {
            await services.EnsureSqlServerDatabaseExistsAsync();
        }
    }
}
