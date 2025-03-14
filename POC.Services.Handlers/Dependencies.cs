namespace POC.Services.Handlers
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using POC.Services.Repositories;
    using System.Reflection;

    public static class Dependencies
    {
        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services, string _configuration)
        {
           return //services.AddMediatR(typeof(Dependencies).Assembly).RegisterRepositories();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Dependencies).Assembly)).RegisterRepositories();

        }
    }
}
