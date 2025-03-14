namespace POC.Logger
{
    using Microsoft.Extensions.DependencyInjection;
    public static class Dependencies
    {
        public static IServiceCollection RegisterLogger(this IServiceCollection services)
        {
            services.AddTransient<ILogger, Logger>();
            return services;
        }
    }
}