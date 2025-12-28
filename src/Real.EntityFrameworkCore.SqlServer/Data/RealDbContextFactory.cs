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
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Real;Trusted_Connection=True;MultipleActiveResultSets=true")
            .UseSqlServerModel();

        return new RealDbContext(optionsBuilder.Options);
    }
}
