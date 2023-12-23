using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DataContext>();
        var connectionString = "";
        
        builder.UseNpgsql(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
        return new DataContext(builder.Options);
    }
}