using Real.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Real.Infrastructure;

public static class SqliteDbModule
{
    public static IServiceCollection AddSqliteDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var dataSource = Path.Combine(appDataPath, "Real.db");

        services.AddDbContext<RealDbContext>(options =>
            options
                .UseSqlite($"Data Source={dataSource}", b => b.MigrationsAssembly("Real.EntityFrameworkCore.Sqlite"))
                .UseSqliteModel());

        //

        return services;
    }

    public static DbContextOptionsBuilder UseSqliteModel(this DbContextOptionsBuilder optionsBuilder)
    {
        return optionsBuilder.ReplaceService<IModelCustomizer, SqliteModelCustomizer>();
    }

    public static void EnsureSqliteDatabaseExists(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<RealDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<RealDbContext>>();

            logger.LogDebug("Starting database migration");

            try
            {
                db.Database.Migrate();

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

    public static async Task EnsureSqliteDatabaseExistsAsync(this IServiceProvider serviceProvider)
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
