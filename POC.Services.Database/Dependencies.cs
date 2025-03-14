namespace RRD.EU.TxQ.Services.Database
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class Dependencies
    {
       public static IServiceCollection RegisterData(this IServiceCollection services, string _configuration)
        {
            services.AddDbContextPool<DbContext>(x => x.UseSqlServer(connectionString: _configuration));
            return services;
        }
    }
}