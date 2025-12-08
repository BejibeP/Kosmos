using Kosmos.Business;
using Kosmos.Domain;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class ServicesExtensions
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IDroitRepository, DroitRepository>();
            services.AddScoped<ITechnologyRepository, TechnologyRepository>();

            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IDroitService, DroitService>();
            services.AddScoped<ITechnologyService, TechnologyService>();

            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                //options.Conventions.Add(new RoutePrefixConvention("api/v1"));
            });
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
