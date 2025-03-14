namespace POC.Services.Repositories
{
	using Microsoft.Extensions.DependencyInjection;
    using POC.Services.Repositories.Interfaces;

	public static class Dependencies
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();               

            return services;
        }
    }
}