using Microsoft.Extensions.DependencyInjection;
using Real.Windows;

namespace Real.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainWindow));
        
        services.AddTransient(typeof(CadastroCategoriasWindow));
        services.AddTransient(typeof(ApuracaoAnualFinancasPorCategoriaWindow));

        return services;
    }
}
