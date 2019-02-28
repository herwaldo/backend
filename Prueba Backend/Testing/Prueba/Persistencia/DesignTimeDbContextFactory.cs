using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistencia.Context;
using System.IO;

namespace Persistencia
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PruebaContext>
    {
        public PruebaContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<PruebaContext>();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseNpgsql(connectionString);
            return new PruebaContext(builder.Options);
        }
    }
}
