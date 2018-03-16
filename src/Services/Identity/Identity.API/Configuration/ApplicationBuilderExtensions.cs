using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace mCore.Services.Identity.API.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(Startup).Assembly);
            });

            var mapper = config.CreateMapper();

            services.Add(new ServiceDescriptor(typeof(IMapper), _ => mapper, ServiceLifetime.Singleton));

            return services;
        }
    }
}
