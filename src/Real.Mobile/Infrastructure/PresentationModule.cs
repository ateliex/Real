using Real.Pages.Categorias;

namespace Real.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainPage));

        services.AddTransient(typeof(CadastroCategoriasPage));
        services.AddTransient(typeof(CategoriaPage));

        return services;
    }
}
