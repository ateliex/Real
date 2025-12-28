using Microsoft.Extensions.Configuration;
using Real.Models;

namespace Real.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSqliteDbServices(configuration);

        services.AddScoped<ApuracaoService>();
        services.AddScoped<FinancasInteligentesProcuder>();

        return services;
    }
}
