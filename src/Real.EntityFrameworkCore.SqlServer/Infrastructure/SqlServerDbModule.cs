using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Real.Data;
using Real.Data.Services;
using Real.Repositories;

namespace Real.Infrastructure;

public static class SqlServerDbModule
{
    public static IServiceCollection AddSqlServerDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        //

        services.AddDbContext<RealDbContext>(options =>
            options
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("Real.EntityFrameworkCore.SqlServer"))
                .UseSqlServerModel());

        services.AddScoped<ContasRepositoryInterface, ContasSqlServerService>();
        services.AddScoped<ApuracoesRepositoryInterface, ApuracoesSqlServerService>();
        services.AddScoped<CategoriasRepositoryInterface, CategoriasSqlServerService>();

        return services;
    }

    public static DbContextOptionsBuilder UseSqlServerModel(this DbContextOptionsBuilder optionsBuilder)
    {
        return optionsBuilder.ReplaceService<IModelCustomizer, SqlServerModelCustomizer>();
    }

    public static void EnsureSqlServerDatabaseExists(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<RealDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<RealDbContext>>();

            logger.LogDebug("Starting database migration");

            try
            {
                db.Database.MigrateAsync();

                logger.LogDebug("Database migration finished");

                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the database" +
                    "Error: {Message}", ex.Message);
            }
        }
    }

    public static async Task EnsureSqlServerDatabaseExistsAsync(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<RealDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<RealDbContext>>();

            logger.LogDebug("Starting database migration");

            try
            {
                await db.Database.MigrateAsync();

                logger.LogDebug("Database migration finished");

                //Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the database" +
                    "Error: {Message}", ex.Message);
            }
        }
    }
}
