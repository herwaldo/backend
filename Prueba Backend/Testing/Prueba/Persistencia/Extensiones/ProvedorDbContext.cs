using Microsoft.EntityFrameworkCore;
using Persistencia.Context;

namespace Microsoft.Extensions.DependencyInjection
{
    //Se inyecta el DbContext
    public static class ProvedorDbContext
    {
        public static IServiceCollection AddProveedorDbContext(this IServiceCollection services, string ConnectionString)
        {
            return services.AddDbContext<PruebaContext>(options => options.UseNpgsql(ConnectionString));
        }

    }
}
