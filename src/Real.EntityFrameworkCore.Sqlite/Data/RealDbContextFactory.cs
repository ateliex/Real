using Real.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Real.Data;

internal class RealDbContextFactory : IDesignTimeDbContextFactory<RealDbContext>
{
    public RealDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RealDbContext>();

        optionsBuilder
            .UseSqlite("Data Source={dataSource}")
            .UseSqliteModel();

        return new RealDbContext(optionsBuilder.Options);
    }
}
