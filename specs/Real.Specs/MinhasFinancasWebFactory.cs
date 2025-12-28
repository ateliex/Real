using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Real.Data;
using Real.Infrastructure;

namespace Real;

public class RealWebFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dateTimeSnapshotDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DateTimeSnapshot));

            services.Remove(dateTimeSnapshotDescriptor);

            services.AddTransient(container => new DateTimeSnapshot(() => DateTime.Now));

            services.AddAuthentication(defaultScheme: "TestScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    "TestScheme", options => { });

            var useLocalDatabase = Environment.GetEnvironmentVariable("UseLocalDatabase") == "true";

            //if (useLocalDatabase)
            {
                //var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RealDbContext>));

                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<RealDbContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");

                    connection.Open();

                    return connection;
                });

                services.AddDbContext<RealDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();

                    options.UseSqlite(connection, b => b.MigrationsAssembly("Real.EntityFrameworkCore.Sqlite"))
                        .UseSqliteModel();

                    //options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            }
        });

        builder.UseEnvironment("Development");
    }
}
