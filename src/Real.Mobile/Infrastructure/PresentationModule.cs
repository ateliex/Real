using Real.Pages.Categorias;
using Real.Pages.Contas;

namespace Real.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainPage));

        services.AddTransient(typeof(CadastroCategoriasPage));
        services.AddTransient(typeof(CategoriaPage));

        services.AddTransient(typeof(GestaoContasPage));
        //services.AddTransient(typeof(ContaPage));

        return services;
    }
}
